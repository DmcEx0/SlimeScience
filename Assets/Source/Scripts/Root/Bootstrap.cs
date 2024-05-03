using Cinemachine;
using SlimeScience.Blocks;
using SlimeScience.Configs;
using SlimeScience.Factory;
using SlimeScience.Saves;
using SlimeScience.Spawners;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Root
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private ReleaseZone _releaseZone;
        [SerializeField] private GeneralPlayerFactory _playerFactory;
        [SerializeField] private GeneralSlimeFactory _slimeFactory;
        [SerializeField] private List<Block> _blocks;

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
            _gameVariables.Load();
        }

        private void Init()
        {
            _gameVariables.Loaded -= Init;

            var player = _playerFactory.Get();

            player.InitGun(_gameVariables);

            _slimeSpawner.Init(player.transform);

            _camera.Follow = player.transform;
            _camera.LookAt = player.transform;

            player.transform.position = Vector3.zero;

            _releaseZone.OpenedNextBlock += OnNextBlockOpened;
            _releaseZone.Init();
        }

        private void OnNextBlockOpened(BlockData blockData, int index)
        {
            Block currentBlock = _blocks[index];

            currentBlock.OpenDoor();
            _slimeSpawner.Spawn(blockData, currentBlock);
        }
    }
}