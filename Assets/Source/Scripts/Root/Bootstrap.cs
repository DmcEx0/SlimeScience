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
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace SlimeScience.Root
{
    public class Bootstrap : MonoBehaviour
    {
        private const float IntervalSaveTime = 30f;

        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private PauseRoot _pauseRoot;

        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private ReleaseZone _releaseZone;
        [SerializeField] private GeneralPlayerFactory _playerFactory;
        [SerializeField] private GeneralSlimeFactory _slimeFactory;
        [SerializeField] private List<Block> _blocks;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        private SlimeSpawner _slimeSpawner;
        private GameVariables _gameVariables;
        private Wallet _wallet;
        private Advertisment _advertisment;
        private Coroutine _intervalSave;

        private PauseHandler _adPause = new PauseHandler();
        private PauseHandler _systemPause = new PauseHandler();

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnBackgroundChange;

            if (_advertisment != null)
            {
                _advertisment.StartIntervalShow();
            }

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

            if (_advertisment != null)
            {
                _advertisment.StopIntervalShow();
            }

            if (_intervalSave != null)
            {
                StopCoroutine(_intervalSave);
            }
        }

        private void Awake()
        {
            _slimeSpawner = new SlimeSpawner(_slimeFactory);
        }

        private void Start()
        {
            _navMeshSurface.BuildNavMesh();
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
            _releaseZone.Init(_wallet, _gameVariables);

            _pauseRoot.Init(new PauseHandler[] { _adPause, _systemPause });

            _advertisment = new Advertisment(this, _adPause);
            _advertisment.StartIntervalShow();

            _intervalSave = StartCoroutine(IntervalSave());

#if UNITY_EDITOR == false
            Agava.YandexGames.YandexGamesSdk.GameReady();
#endif
        }

        private void OnNextBlockOpened(BlockData blockData, int index)
        {
            Block currentBlock = _blocks[index];

            currentBlock.OpenDoor();
            _slimeSpawner.Spawn(blockData, currentBlock);

            _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
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
    }
}