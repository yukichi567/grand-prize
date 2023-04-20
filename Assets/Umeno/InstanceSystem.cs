using UnityEngine;

public class InstanceSystem<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                var typeName = typeof(T);
                _instance = FindObjectOfType<T>();
                if(_instance == null)
                {
                    Debug.LogError($"{typeName}В™СґНЁВµВ№ВєВс");
                }
            }
            return _instance;
        }
    }
}