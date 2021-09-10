using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    public static T Instance { private set; get; }

    public virtual void Awake()
    {
        InitInstance();
    }
    public virtual void InitInstance()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
