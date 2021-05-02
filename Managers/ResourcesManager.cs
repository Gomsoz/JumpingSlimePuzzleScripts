using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public void Instantiate (string path, Transform parent = null)
    {
        GameObject go = Load<GameObject>(path);
        GameObject.Instantiate(go, parent);

    }
}
