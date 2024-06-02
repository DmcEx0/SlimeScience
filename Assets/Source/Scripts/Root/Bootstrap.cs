using Cinemachine;
using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using SlimeScience.Money;
using SlimeScience.Saves;
using SlimeScience.Spawners;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace SlimeScience.Root
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private UIRoot _uiRoot;

        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private ReleaseZone _releaseZone;
        [SerializeField] private GeneralPlayerFactory _playerFactory;
        [SerializeField] private GeneralSlimeFactory _slimeFactory;
        [SerializeField] private List<Block> _blocks;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        private SlimeSpawner _slimeSpawner;
        private GameVariables _gameVariables;
        private Wallet _wallet;

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
            _releaseZone.Init(_wallet);

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
    }
}