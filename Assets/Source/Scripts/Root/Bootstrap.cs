using Cinemachine;
using SlimeScience.Configs;
using SlimeScience.Factory;
using SlimeScience.Saves;
using SlimeScience.Spawners;
using UnityEngine;

namespace SlimeScience.Root
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private UIRoot _uiRoot;

        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private ReleaseZone _releaseZone;
        [SerializeField] private GeneralPlayerFactory _playerFactory;
        [SerializeField] private GeneralSlimeFactory _slimeFactory;

        private SlimeSpawner _slimeSpawner;
        private GameVariables _gameVariables;

        private void OnDisable()
        {
            _releaseZone.OpenedNextBlock -= OnNextBlockOpened;
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
            _uiRoot.Init();

            _gameVariables.Loaded -= Init;

            var player = _playerFactory.Get();

            player.InitGun(_gameVariables);

            _slimeSpawner.Init(player.transform);

            _camera.Follow = player.transform;
            _camera.LookAt = player.transform;

            player.transform.position = Vector3.zero;

            _releaseZone.OpenedNextBlock += OnNextBlockOpened;
            _releaseZone.Init();

#if UNITY_EDITOR == false
            Agava.YandexGames.YandexGamesSdk.GameReady();
#endif
        }

        private void OnNextBlockOpened(BlockData blockData)
        {
            _slimeSpawner.Spawn(blockData);
        }
    }
}