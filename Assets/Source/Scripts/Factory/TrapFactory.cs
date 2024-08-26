using SlimeScience.Configs;
using SlimeScience.Traps;
using UnityEngine;

namespace SlimeScience.Factory
{
    public abstract class TrapFactory : GameObjectFactory
    {
        public Trap Get(Vector3 position)
        {
            var config = GetNextConfig();
            var instance = CreateInstance(config.Prefab, position);

            instance.Init(config);

            return instance;
        }

        protected abstract TrapConfig GetNextConfig();
    }
}
