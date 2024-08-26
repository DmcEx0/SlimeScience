using SlimeScience.Characters;
using UnityEngine;

namespace SlimeScience.Tutorial
{
    [RequireComponent(typeof(Collider))]
    public class Trigger1 : MonoBehaviour
    {
        [SerializeField] private TutorialManger _tutorial;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _tutorial.ShowPopupForTrigger1();
            }
        }
    }
}
