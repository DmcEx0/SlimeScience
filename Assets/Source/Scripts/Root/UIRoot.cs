using SlimeScience.Input;
using UnityEngine;

namespace SlimeScience.Root
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick _floatingJoystick;

        public void Init()
        {
            _floatingJoystick.Init();
        }
    }
}
