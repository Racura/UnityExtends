using UnityEngine;
using System.Collections;
using System;

public class PoolableObject : MonoBehaviour, IPoolableObject
{
	public virtual bool IsActive { get { return isActiveAndEnabled || prepared; } }
	public virtual ulong ResetId { get { return resetId; } }
    
    private IPoolableObject prefab;
	ulong resetId;

    IPoolableObject IPoolableObject.Prefab { get { return prefab;  } }
    void IPoolableObject.SetPrefab(IPoolableObject prefab) {
        if (this.prefab != null)
            Debug.LogError("Prefab already set.");
        this.prefab = prefab;
    }
	bool prepared;

	protected virtual void Awake()
    {
		prepared = false;
    }

	protected virtual void OnEnable ()
	{
		if(Application.isPlaying && !prepared) {
			gameObject.SetActive(prepared);
		}
	}

	public virtual void Prepare(ulong resetId)
	{
		if(prepared) {
			Debug.LogError("ResettableObject is already prepared.");
		}

		if(IsActive) {
			Debug.LogError("ResettableObject is already active.");
		}

		this.resetId = resetId;

		prepared = true;
		SendMessage("PrepareObject", SendMessageOptions.DontRequireReceiver);
	}

    public virtual void Restore() {
		if(!Application.isPlaying) return;

		if(!prepared) {
			Debug.LogError("ResettableObject has not been prepared.");
		}

        gameObject.SetActive(true);
		SendMessage("RestartObject", SendMessageOptions.DontRequireReceiver);
    }

    public virtual void ReturnToPool()
    {
		gameObject.SetActive(false);
		prepared = false;
    }
}

public interface IPoolableObject {
    bool IsActive { get; }
	ulong ResetId { get; }

    IPoolableObject Prefab { get; }
    void SetPrefab (IPoolableObject prefab);

	void Prepare (ulong resetId);

    void Restore ();

    void ReturnToPool();
}
