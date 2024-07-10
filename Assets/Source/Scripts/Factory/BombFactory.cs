using SlimeScience.Configs;
using SlimeScience.Pool;
using SlimeScience.Traps;
using UnityEngine;

namespace SlimeScience.Factory
{
    public abstract class BombFactory : GameObjectFactory
    {
        private ObjectPool<Bomb> _pool;
        private BombConfig _config;

        public void CreatePool(int allBobmsCount, Transform parent)
        {
            _config = GetConfig();
            _pool= new ObjectPool<Bomb>(_config.Prefab, allBobmsCount, parent);
        }
        
        public Bomb Get(Vector3 position)
        {
            var bomb = _pool.GetAvailable();
            bomb.transform.position = position;
            
            return bomb;
        }

        protected abstract BombConfig GetConfig();

    }
}
