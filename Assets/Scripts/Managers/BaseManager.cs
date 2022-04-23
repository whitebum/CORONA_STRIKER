using System;
using UnityEngine;

public abstract class BaseManager<T> : MonoBehaviour where T : class 
{
    private static Lazy<T> _instance = new Lazy<T> (() => 
    {
        var instance = FindObjectOfType(typeof(T)) as T;

        if (instance == null)
        {
            instance = new GameObject("New Manager").AddComponent(typeof(T)) as T;

            DontDestroyOnLoad((instance as MonoBehaviour).gameObject);
        }

        return instance;
    });

    public static T GetInstance()
    {
        return _instance.Value;
    }
}
