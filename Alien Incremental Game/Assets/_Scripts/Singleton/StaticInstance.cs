using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        Instance = this as T;
    }
    protected virtual void OnapplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
