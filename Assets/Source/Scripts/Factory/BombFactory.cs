using SlimeScience.Configs;
using SlimeScience.Pool;
using SlimeScience.Traps;
using UnityEngine;

namespace SlimeScience.Factory
{
    public abstract class BombFactory : GameObjectFactory
    {
        private ObjectPool<Bomb> _pool;

        public void CreatePool(int allBobmsCount, Transform parent)
        {
            _pool = new ObjectPool<Bomb>(GetConfig().Prefab, allBobmsCount, parent);
        }

        public Bomb Get(Vector3 position)
        {
            var bomb = _pool.GetAvailable();
            bomb.transform.position = position;
            bomb.gameObject.SetActive(true);

            return bomb;
        }

        protected abstract BombConfig GetConfig();
    }
}
