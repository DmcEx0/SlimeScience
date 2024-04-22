using UnityEngine;

public abstract class GameObjectFactory : ScriptableObject
{
    protected T CreateInstance<T>(T prefab) where T : MonoBehaviour
    {
        var instance = Instantiate(prefab);

        return instance;
    }
}