using Agava.WebUtility;
using Cinemachine;
using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using SlimeScience.Money;
using SlimeScience.PauseSystem;
using SlimeScience.Root;
using SlimeScience.Saves;
using SlimeScience.Spawners;
using SlimeScience.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Tutorial
{
    public class BootstrapTutorial : MonoBehaviour
    {
        private const float IntervalSaveTime = 30f;

        [SerializeField] private TutorialManger _tutorialManger;
        [SerializeField] private PauseRoot _pauseRoot;

        [SerializeField] private UIRoot _uIRoot;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private ReleaseZone _releaseZone;
        [SerializeField] private GeneralPlayerFactory _playerFactory;
        [SerializeField] private GeneralSlimeFactory _slimeFactory;
        [SerializeField] private GeneralBombFactory _bombFactory;
        [SerializeField] private GeneralTrapFactory _trapFactory;
        [SerializeField] private List<Block> _blocks;
        [SerializeField] private BlocksConfig _blocksConfig;

        [SerializeField] private SoundsConfig _soundsConfig;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _sfxAudioSource;

        private TrapSpawner _trapSpawner;
        private SlimeSpawner _slimeSpawner;
        private BombSpawner _bombSpawner;
        private GameVariables _gameVariables;
        private Wallet _wallet;
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
            }
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChange;

            _releaseZone.OpenedNextBlock -= OnNextBlockOpened;

            if (_intervalSave != null)
            {
                StopCoroutine(_intervalSave);
            }
        }

        private void Awake()
        {
            _uIRoot.ShowLoading();
            SoundsManager.Initialize(_soundsConfig, _audioSource, _sfxAudioSource);
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

        private void Init()
        {
            _gameVariables.Loaded -= Init;

            _slimeSpawner = new SlimeSpawner(_slimeFactory, _gameVariables);
            _bombSpawner = new BombSpawner(_bombFactory, transform, GetAllBombsCount());
            _trapSpawner = new TrapSpawner(_trapFactory);
            _wallet = new Wallet(_gameVariables);

            var player = _playerFactory.Get();
            player.InitGun(_gameVariables);
            _camera.Follow = player.transform;
            _camera.LookAt = player.transform;
            player.transform.position = Vector3.zero;

            _tutorialManger.Init(player, _gameVariables);

            _slimeSpawner.Init(player.transform, transform);

            _releaseZone.OpenedNextBlock += OnNextBlockOpened;
            _releaseZone.Init(_wallet, _gameVariables, _blocksConfig);

            _pauseRoot.Init(new PauseHandler[] { _adPause, _systemPause });

            _uIRoot.Init(_wallet, _gameVariables, null, _adPause, player);
            _uIRoot.HideLoading();
#if UNITY_EDITOR == false
            Agava.YandexGames.YandexGamesSdk.GameReady();
#endif
        }

        private void OnNextBlockOpened(BlockData blockData, int index)
        {
            Block currentBlock = _blocks[index];

            currentBlock.OpenDoor();
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

        private int GetAllSlimesCount() //TODO: remove code dubbing
        {
            int count = 0;

            foreach (var block in _blocksConfig.BlocksData)
            {
                count += block.SlimeAmount;
            }

            return count;
        }

        private int GetAllBombsCount() //TODO: remove code dubbing
        {
            int count = 0;

            foreach (var block in _blocksConfig.BlocksData)
            {
                count += block.BombAmount;
            }

            return count;
        }
    }
}
