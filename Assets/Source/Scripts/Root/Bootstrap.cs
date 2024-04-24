using SlimeScience.Factory;
using UnityEngine;

namespace SlimeScience.Root
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GeneralPlayerFactory _playerFactory;
        [SerializeField] private GeneralSlimeFactory _slimeFactory;

        [SerializeField] private int _slimeCount;
        [SerializeField] private float _rangePosX;
        [SerializeField] private float _rangePosZ;

        private void Start()
        {
            var player = _playerFactory.Get();
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