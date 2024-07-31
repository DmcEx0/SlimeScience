using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Pool
{
    public class ObjectPool<T>
        where T : MonoBehaviour
    {
        private T _prefab;
        private int _poolSize;
        private Transform _parent;

        private List<T> _pool;

        public ObjectPool(T prefab, int poolSize, Transform parent)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _parent = parent;
            _pool = InitializePool();
        }

        public ObjectPool(Transform parent)
        {
            _parent = parent;
            _pool = new List<T>();
        }

        public T GetAvailable()
        {
            foreach (var instance in _pool)
            {
                if (!instance.gameObject.activeSelf)
                {
                    return instance;
                }
            }

            var newInstance = GameObject.Instantiate(_prefab, _parent);
            _pool.Add(newInstance);
            return newInstance;
        }

        public void Reset()
        {
            foreach (var instance in _pool)
            {
                instance.gameObject.SetActive(false);
            }
        }
        
        public void InitializePool(T gameObject)
        {
            gameObject.transform.parent = _parent;
            _pool.Add(gameObject);
            gameObject.gameObject.SetActive(false);
        }

        private List<T> InitializePool()
        {
            var pool = new List<T>();

            for (int i = 0; i < _poolSize; i++)
            {
                var instance = GameObject.Instantiate(_prefab, _parent);
                pool.Add(instance);
                instance.gameObject.SetActive(false);
            }

            return pool;
        }
    }
}