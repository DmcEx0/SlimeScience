using SlimeScience.Factory;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience.Root
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private UIRoot _uiRoot;

        [SerializeField] private GeneralPlayerFactory _playerFactory;
        [SerializeField] private GeneralSlimeFactory _slimeFactory;

        [SerializeField] private int _slimeCount;
        [SerializeField] private float _rangePosX;
        [SerializeField] private float _rangePosZ;

        private GameVariables _gameVariables;

        private void Start()
        {
            _gameVariables = new GameVariables();

            _gameVariables.Loaded += Init;
            _gameVariables.Load();
        }

        private void Init()
        {
            _uiRoot.Init();

            _gameVariables.Loaded -= Init;

            var player = _playerFactory.Get();
            player.InitGun(_gameVariables);

            player.transform.position = Vector3.zero;

            for (int i = 0; i < _slimeCount; i++)
            {
                float randomPosX = Random.Range(-_rangePosX, _rangePosX);
                float randomPosZ = Random.Range(-_rangePosZ, _rangePosZ);
                Vector3 newPos = new Vector3(randomPosX, 0, randomPosZ);

                var newSlime = _slimeFactory.Get(player.transform);
                newSlime.transform.position = newPos;
            }
        }
    }
}