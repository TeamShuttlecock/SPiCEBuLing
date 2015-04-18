using UnityEngine;
using System.Collections.Generic;

public static class MonoBehaviourExtension
{
    static public T GetOrAddComponent<T>(this Component child) where T : Component
    {
        T result = child.GetComponent<T>();
        if (result == null)
        {
            result = child.gameObject.AddComponent<T>();
        }
        return result;
    }

    static public void SetComponentEnabled<T>(this Transform tr, bool f) where T : MonoBehaviour
    {
        T result = tr.GetComponent<T>();
        if (result != null)
            result.enabled = f;
    }

    static public List<T> GetComponentsRecursively<T>(this Transform tr) where T : MonoBehaviour
    {
        List<T> ret = new List<T>();
        GetComponentsOf<T>(tr, ref ret);
        return ret;
    }

    static void GetComponentsOf<T>(Transform tr, ref List<T> ret) where T : MonoBehaviour
    {
        ret.AddRange(tr.GetComponents<T>());
        for(int i = 0; i < tr.childCount; i++)
        {
            GetComponentsOf<T>(tr.GetChild(i), ref ret);
        }
    }
}
