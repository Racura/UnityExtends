using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : ProtectedSingleton<T>
	where T : Singleton<T>
{
    public new static T Instance {
		get { return ProtectedSingleton<T>.Instance; }
	}
}


public abstract class ProtectedSingleton<T> : MonoBehaviour
	where T : ProtectedSingleton<T>
{
    public static bool HasInstance { get { return instance != null; } }

    public bool IsInstance { get { return this == instance; } }

    static T instance;
	protected static T Instance {
		get {
			if(instance == null){
				instance = FindObjectOfType<T>();
			}
			return instance;
		}
	}
    
    public static void Create()
    {
        var name = typeof(T).Name;

        if (instance != null) {
            Debug.LogWarning(name + " is not null, creating new instance");
        }

        var gameObject = new GameObject(name, typeof(T)) {
			hideFlags = HideFlags.DontSaveInBuild,
        };

        instance = gameObject.GetComponent<T>();
    }

	protected virtual void Awake () {
		instance = this as T;
	}
	
	protected virtual void OnDestroy () {
		if(instance == this) {
			instance = null;
		}
	}
}

public abstract class PersistentSingleton<T> : ProtectedSingleton<T>
    where T : PersistentSingleton<T>
{	
	protected override void Awake () {
		if(!HasInstance || Instance == this) {
            base.Awake();
			GameObject.DontDestroyOnLoad(this.gameObject);
			GameObject.DontDestroyOnLoad(this);
		} else {
			GameObject.DestroyObject(this.gameObject);
		}
	}
}