using SlimeScience.Configs;
using SlimeScience.Effects;
using UnityEngine;

namespace SlimeScience.Traps
{
    [RequireComponent(typeof(Collider))]
    public class Trap : MonoBehaviour
    {
        public TrapConfig Config { get; private set; }
        
        public void Init(TrapConfig config)
        {
            Config = config;
        }
    }
}
