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
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Root
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private PauseRoot _pauseRoot;

        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private ReleaseZone _releaseZone;
        [SerializeField] private GeneralPlayerFactory _playerFactory;
        [SerializeField] private GeneralSlimeFactory _slimeFactory;
        [SerializeField] private List<Block> _blocks;

        private SlimeSpawner _slimeSpawner;
        private GameVariables _gameVariables;
        private Wallet _wallet;
        private Advertisment _advertisment;

        private PauseHandler _adPause = new PauseHandler();
        private PauseHandler _systemPause = new PauseHandler();

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnBackgroundChange;

            if (_advertisment != null)
            {
                _advertisment.StartIntervalShow();
            }
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChange;

            _releaseZone.OpenedNextBlock -= OnNextBlockOpened;
            
            if (_advertisment != null)
            {
                _advertisment.StopIntervalShow();
            }
        }

        private void Awake()
        {
            _slimeSpawner = new SlimeSpawner(_slimeFactory);
        }

        private void Start()
        {
            _gameVariables = new GameVariables();

            _gameVariables.Loaded += Init;
            _gameVariables.Load(this);
        }

        private void Init()
        {
            _gameVariables.Loaded -= Init;

            _wallet = new Wallet(_gameVariables);
            _uiRoot.Init(_wallet, _gameVariables);

            var player = _playerFactory.Get();

            player.InitGun(_gameVariables);

            _slimeSpawner.Init(player.transform);

            _camera.Follow = player.transform;
            _camera.LookAt = player.transform;

            player.transform.position = Vector3.zero;

            _releaseZone.OpenedNextBlock += OnNextBlockOpened;
            _releaseZone.Init(_wallet);

            _pauseRoot.Init(new PauseHandler[] { _adPause, _systemPause });

            _advertisment = new Advertisment(this, _adPause);
            _advertisment.StartIntervalShow();

#if UNITY_EDITOR == false
            Agava.YandexGames.YandexGamesSdk.GameReady();
#endif
        }

        private void OnNextBlockOpened(BlockData blockData, int index)
        {
            Block currentBlock = _blocks[index];

            currentBlock.OpenDoor();
            _slimeSpawner.Spawn(blockData, currentBlock);
        }

        private void OnBackgroundChange(bool isInBackground)
        {
            Debug.Log("Background change");

            if (isInBackground)
            {
                _systemPause.Pause();
            }
            else
            {
                _systemPause.Unpause();
            }
        }
    }
}