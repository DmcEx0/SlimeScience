using UnityEngine;

namespace SlimeScience.Factory
{
    public abstract class GameObjectFactory : ScriptableObject
    {
        protected T CreateInstance<T>(T prefab, Vector3 position) where T : MonoBehaviour
        {
            var instance = Instantiate(prefab, position, Quaternion.identity);

            return instance;
        }
    }
}