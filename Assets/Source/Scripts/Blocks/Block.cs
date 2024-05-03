using UnityEngine;

namespace SlimeScience
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private Transform _door;
        [SerializeField] private Transform _centre;

        public Transform Centre => _centre;

        public void OpenDoor()
        {
            if (_door != null)
                _door.gameObject.SetActive(false);
        }
    }
}
