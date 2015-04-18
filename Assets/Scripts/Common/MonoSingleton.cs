using UnityEngine;

/// Toolbox is a real singleton.
class Toolbox : MonoBehaviour
{
    private static Toolbox _instance = null;
    // don't drop shit into the editor when quiting.
    private static bool applicationIsQuitting = false;

    private static object _lock = new object();

    private Toolbox() { }

    public static Toolbox Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("application is quiting, won't create Toolbox");
                return null;
            }

            lock (_lock)
            {
                _instance = _instance ?? FindObjectOfType<Toolbox>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<Toolbox>();
                    singleton.name = "_singleton_toolbox_";

                    DontDestroyOnLoad(singleton);
                }

                return _instance;
            }
        }
    }

    public T RegisteComponent<T>() where T : MonoBehaviour
    {
        return Instance.GetOrAddComponent<T>();
    }


    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}

/// <summary>
/// said it's a singleton, actually it's a toolbox(toolbox is a real singleton) which is better.
/// notice that you're responsible for creating the private constructor for the subclass of singletons to insure that there is only one instance.
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            _instance = _instance ?? Toolbox.Instance.RegisteComponent<T>();
            return _instance;
        }
    }
}