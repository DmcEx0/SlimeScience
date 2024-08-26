using UnityEngine;

namespace SlimeScience.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private Transform _door;
        [SerializeField] private Transform _centre;
        [SerializeField] private Transform _trapsParent;

        public Transform Centre => _centre;

        public Transform TrapsParent => _trapsParent;

        public bool IsOpened { get; private set; }

        public void OpenDoor()
        {
            if (_door != null)
            {
                _door.gameObject.SetActive(false);
                IsOpened = true;
            }
        }
    }
}
