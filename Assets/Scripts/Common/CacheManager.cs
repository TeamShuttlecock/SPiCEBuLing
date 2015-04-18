using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CacheManager : MonoSingleton<CacheManager>
{
    protected Dictionary<string, GameObject> mCachedPrefabs = new Dictionary<string, GameObject>();

    // prefab name - gameobject list
    protected Dictionary<string, List<GameObject>> mCachedGameObjects = new Dictionary<string, List<GameObject>>();
    protected Dictionary<string, int> mCachedGameObjectsAvailableCount = new Dictionary<string, int>();
    protected List<string> mPrefabWaitingForCache = new List<string>();

    private static Transform _cachedGameObjectsRoot = null;
    private static Transform AllCachedGameObjectsRoot
    {
        get
        {
            if (_cachedGameObjectsRoot == null)
            {
                GameObject go = new GameObject("AllCachedGameObjects");
                _cachedGameObjectsRoot = go.GetComponent<Transform>();
                if (_cachedGameObjectsRoot == null)
                    _cachedGameObjectsRoot = go.AddComponent<Transform>();
                _cachedGameObjectsRoot.position = Vector3.zero;
                _cachedGameObjectsRoot.localScale = Vector3.one;
            }
            return _cachedGameObjectsRoot;
        }
    }

    public void Awake()
    {
        ClearCache();
    }

    public void OnDestroy()
    {
        ClearCache();
    }

    public void ClearCache()
    {
        mCachedPrefabs.Clear();
        mCachedGameObjects.Clear();
        mCachedGameObjectsAvailableCount.Clear();
        mPrefabWaitingForCache.Clear();
    }

    public GameObject GetCachedPrefab(string abid)
    {
        if(mCachedPrefabs.ContainsKey(abid))
        {
            return mCachedPrefabs[abid];
        }

        return null;
    }

    public void AddCachedPrefab(string abid, GameObject prefab)
    {
        if(!mCachedPrefabs.ContainsKey(abid))
        {
            mCachedPrefabs.Add(abid, prefab);
        }
    }

    public Object InstantiateObject(Object original)
    {
        return InstantiateObject(original, Vector3.zero, Quaternion.identity);
    }

    public Object InstantiateObject(Object original, Vector3 position, Quaternion rotation)
    {
        GameObject rel = null;

        if(!mCachedGameObjects.ContainsKey(original.name))
        {
            List<GameObject> gameObjectsCache = new List<GameObject>();

            GameObject obj = (GameObject)GameObject.Instantiate(original);
            obj.SetActive(false);
            obj.transform.SetParent(AllCachedGameObjectsRoot);
            gameObjectsCache.Add(obj);

            mCachedGameObjects.Add(original.name, gameObjectsCache);
            mCachedGameObjectsAvailableCount.Add(original.name, 1);
        }

        List<GameObject> gameObjects = mCachedGameObjects[original.name];
        int availableCount = mCachedGameObjectsAvailableCount[original.name];

        if (gameObjects.Count > 0)
        {
            if (availableCount <= 0)
            {
                GameObject obj = (GameObject)GameObject.Instantiate(original);
                obj.SetActive(false);
                obj.transform.SetParent(AllCachedGameObjectsRoot);
                mCachedGameObjects[original.name].Insert(0, obj);
                mCachedGameObjectsAvailableCount[original.name]++;
                availableCount++;

                if(!mPrefabWaitingForCache.Contains(original.name))
                {
                    StartCoroutine(CreateGameObjects(original, gameObjects.Count));
                    mPrefabWaitingForCache.Add(original.name);
                }
            }

            rel = gameObjects[availableCount - 1];
            mCachedGameObjectsAvailableCount[original.name]--;
            rel.transform.SetParent(null);
            rel.transform.position = position;
            rel.transform.rotation = rotation;
            rel.SetActive(true);
        }

        return rel;
    }

    protected IEnumerator CreateGameObjects(Object prefab, int number)
    {
        if(number <= 0)
        {
            yield break;
        }

        for (int i = 0; i < number; i++)
        {
            yield return CreateGameObject(prefab);
        }

        mPrefabWaitingForCache.Remove(prefab.name);
    }

    protected IEnumerator CreateGameObject(Object prefab)
    {
        GameObject obj = (GameObject)GameObject.Instantiate(prefab);
        obj.SetActive(false);
        obj.transform.SetParent(AllCachedGameObjectsRoot);
        //mCachedGameObjects[prefab.name].Insert(0, obj);
        if (!mCachedGameObjects.ContainsKey(prefab.name))
        {
            List<GameObject> gameObjectsCache = new List<GameObject>();

            gameObjectsCache.Add(obj);

            mCachedGameObjects.Add(prefab.name, gameObjectsCache);
            mCachedGameObjectsAvailableCount.Add(prefab.name, 1);
        }
        else
        {
            mCachedGameObjects[prefab.name].Insert(0, obj);
            mCachedGameObjectsAvailableCount[prefab.name]++;
        }

        yield return null;
    }

    public void DestroyCachedObject(Object obj)
    {
        DestroyCachedObject(obj, 0);
    }

    public void DestroyCachedObject(Object obj, float t)
    {
        if(obj != null)
        {
            string prefabName = obj.name.Replace("(Clone)", "");
            if (mCachedGameObjects.ContainsKey(prefabName))
            {
                StartCoroutine(DestroyObjectImpl(obj, t));
            }
            else
            {
                GameObject.Destroy(obj, t);
            }
        }
    }

    protected IEnumerator DestroyObjectImpl(Object obj, float t)
    {
        yield return new WaitForSeconds(t);

        if(obj != null)
        {
            string prefabName = obj.name.Replace("(Clone)", "");

            GameObject gameObj = obj as GameObject;
            List<GameObject> gameObjects = mCachedGameObjects[prefabName];
            int availableCount = mCachedGameObjectsAvailableCount[prefabName];
            int idx = gameObjects.IndexOf(gameObj);

            gameObj.SetActive(false);
            gameObj.transform.SetParent(AllCachedGameObjectsRoot);
            gameObj.transform.position = Vector3.zero;
            gameObj.transform.rotation = Quaternion.identity;

            if (availableCount < gameObjects.Count)
            {
                GameObject tmp = gameObjects[idx];
                gameObjects[idx] = gameObjects[availableCount];
                gameObjects[availableCount] = tmp;

                mCachedGameObjectsAvailableCount[prefabName]++;
            }
        }
    }
}
