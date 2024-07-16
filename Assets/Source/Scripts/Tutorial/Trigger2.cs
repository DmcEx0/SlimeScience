using SlimeScience.Characters;
using UnityEngine;

namespace SlimeScience.Tutorial
{
    public class Trigger2 : MonoBehaviour
    {
        private const string TutorialCompleted = "Tutorial";

        [SerializeField] private TutorialManger _tutorial;
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player player))
            {
                PlayerPrefs.SetInt("Tutorial", 1);
                _tutorial.GoToGameScene();
            }
        }
    }
}
