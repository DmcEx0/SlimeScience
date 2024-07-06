using SlimeScience.Configs;
using SlimeScience.Traps;
using UnityEngine;

namespace SlimeScience.Factory
{
    public abstract class BombFactory : GameObjectFactory
    {
        public Bomb Get(Vector3 position)
        {
            var config = GetConfig();
            Bomb instance = CreateInstance(config.Prefab, position);

            return instance;
        }

        protected abstract BombConfig GetConfig();

    }
}
