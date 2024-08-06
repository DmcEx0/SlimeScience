using System;
using Agava.WebUtility;
using Cinemachine;
using SlimeScience.Ad;
using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using SlimeScience.Money;
using SlimeScience.PauseSystem;
using SlimeScience.Saves;
using SlimeScience.Spawners;
using System.Collections;
using System.Collections.Generic;
using SlimeScience.Characters.Ship;
using SlimeScience.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SlimeScience.Root
{
    public class Bootstrap : MonoBehaviour
    {
        private const float IntervalSaveTime = 30f;
        private const string GameSceneName = "Game";

        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private PauseRoot _pauseRoot;

        [SerializeField] private Ship _ship;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private ReleaseZone _releaseZone;
        [SerializeField] private GeneralPlayerFactory _playerFactory;
        [SerializeField] private GeneralSlimeFactory _slimeFactory;
        [SerializeField] private GeneralVacuumingSupportFactory _vacuumingSupportFactory;
        [SerializeField] private GeneralBombFactory _bombFactory;
        [SerializeField] private GeneralTrapFactory _trapFactory;
        [SerializeField] private List<Block> _blocks;
        [SerializeField] private BlocksConfig _blocksConfig;

        [SerializeField] private SoundsConfig _soundsConfig;
        [SerializeField] private AudioSource _audioSource;

        private TrapSpawner _trapSpawner;
        private SlimeSpawner _slimeSpawner;
        private BombSpawner _bombSpawner;
        private GameVariables _gameVariables;
        private Wallet _wallet;
        private Advertisment _advertisment;
        private Coroutine _intervalSave;

        private PauseHandler _adPause = new PauseHandler();
        private PauseHandler _systemPause = new PauseHandler();

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnBackgroundChange;

            if (_intervalSave != null)
            {
                StopCoroutine(_intervalSave);
            }

            if (_gameVariables != null)
            {
                _intervalSave = StartCoroutine(IntervalSave());
                _gameVariables.AssistantUpgraded += OnAssistantUpgraded;
            }

            _releaseZone.PlayerReleased += OnReleased;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChange;

            _releaseZone.OpenedNextBlock -= OnNextBlockOpened;
            _releaseZone.LastBlockClosed -= OnLastBlockClosed;

            if (_intervalSave != null)
            {
                StopCoroutine(_intervalSave);
            }

            if (_gameVariables != null)
            {
                _gameVariables.AssistantUpgraded -= OnAssistantUpgraded;
            }

            _releaseZone.PlayerReleased -= OnReleased;
        }

        private void Awake()
        {
            _uiRoot.ShowLoading();
            SoundsManager.Initialize(_soundsConfig, _audioSource);
        }

        private void Start()
        {
            _gameVariables = new GameVariables();

            _gameVariables.Loaded += Init;
            _gameVariables.Load(this);

            SoundsManager.PlayBgMusic();
        }

        public void ResetSaves()
        {
            PlayerPrefs.DeleteAll();
            _gameVariables.ResetSave();
        }
        
        private void ResetSaveOnFinishGame()
        {
            PlayerPrefs.DeleteAll();
            _gameVariables.ResetSaveOnFinishGame();
        }
        
        private void Init()
        {
            _gameVariables.Loaded -= Init;

            _advertisment = new Advertisment(this, _adPause);
            _slimeSpawner = new SlimeSpawner(_slimeFactory, _gameVariables);
            _bombSpawner = new BombSpawner(_bombFactory, transform, GetAllBombsCount());
            _trapSpawner = new TrapSpawner(_trapFactory);
            
            _wallet = new Wallet(_gameVariables);

            var player = _playerFactory.Get();
            player.InitGun(_gameVariables);
            _camera.Follow = player.transform;
            _camera.LookAt = player.transform;
            player.transform.position = Vector3.zero;

            _slimeSpawner.Init(player.transform, transform);

            _releaseZone.OpenedNextBlock += OnNextBlockOpened;
            _releaseZone.LastBlockClosed += OnLastBlockClosed;
            _releaseZone.Init(_wallet, _gameVariables, _blocksConfig);

            _pauseRoot.Init(new PauseHandler[] { _adPause, _systemPause });

            _intervalSave = StartCoroutine(IntervalSave());

            for (int i = 0; i < _gameVariables.AbsorptionAssistantCount; i++)
            {
                var vacuumingSupport = _vacuumingSupportFactory.Get(Vector3.zero, _releaseZone.transform);
                vacuumingSupport.InitGun(_gameVariables);
                vacuumingSupport.SetUnloadPosition(_releaseZone.transform.position);
            }

            _gameVariables.AssistantUpgraded += OnAssistantUpgraded;
            _ship.Init(_gameVariables);
            _uiRoot.Init(_wallet, _gameVariables, _advertisment, _adPause, player);

            _uiRoot.HideLoading();
#if UNITY_EDITOR == false
            Agava.YandexGames.YandexGamesSdk.GameReady();
#endif
        }

        private void OnNextBlockOpened(BlockData blockData, int index)
        {
            if (index != 0)
            {
                _uiRoot.BlocksPopupsManager.ShowPopup(index);
            }

            Block currentBlock = _blocks[index];

            for (int i = 0; i <= index; i++)
            {
                if (_blocks[i].IsOpened == false)
                {
                    _blocks[i].OpenDoor();
                }
            }

            _slimeSpawner.Spawn(blockData, currentBlock);
            _bombSpawner.Spawn(blockData, currentBlock);
            _trapSpawner.Spawn(blockData, currentBlock);
        }

        private void OnBackgroundChange(bool isInBackground)
        {
            if (isInBackground)
            {
                _systemPause.Pause();
            }
            else
            {
                _systemPause.Unpause();
            }
        }

        private IEnumerator IntervalSave()
        {
            var delay = new WaitForSeconds(IntervalSaveTime);

            while (gameObject.activeSelf)
            {
                yield return delay;
                _gameVariables.Save();
            }
        }

        private int GetAllBombsCount()
        {
            int count = 0;

            foreach (var block in _blocksConfig.BlocksData)
            {
                count += block.BombAmount;
            }

            return count;
        }

        private void OnLastBlockClosed()
        {
            _uiRoot.BlocksPopupsManager.ShowEndGamePopup(ResetGame);
        }

        private void ResetGame()
        {
            ResetSaveOnFinishGame();
            SceneManager.LoadScene(GameSceneName);
        }

        private void OnReleased()
        {
            _uiRoot.ShowInterstitial();
        }

        private void OnAssistantUpgraded(float count)
        {
            var vacuumingSupport = _vacuumingSupportFactory.Get(Vector3.zero, _releaseZone.transform);
            vacuumingSupport.InitGun(_gameVariables);
            vacuumingSupport.SetUnloadPosition(_releaseZone.transform.position);
        }
    }
}