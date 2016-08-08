using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class PoolableManager : ProtectedSingleton<PoolableManager> {   
	
	Dictionary<IPoolableObject, List<IPoolableObject>> dic;
	int counter;

	ulong resetIdCounter;

	void ConstuctCollection () {
		dic = new Dictionary<IPoolableObject, List<IPoolableObject>>();

		var collection = GetComponentsInChildren<IPoolableObject>(true);

		for(int i = 0; i < collection.Length; ++i) {
	
			var obj = collection[i];
			
			if(obj.Prefab == null)
				continue;

			if(!dic.ContainsKey(obj.Prefab))
				dic.Add(obj.Prefab, new List<IPoolableObject>());
		
			var list = dic[obj.Prefab];

			if(!list.Contains(obj))
				list.Add(obj);
		}
	}



	public static T GetResettable<T> (T prefab, bool prepare = false)
		where T : class, IPoolableObject
	{
		if(prefab == null) return null;
		if (!HasInstance) Create();

		var obj = Instance._Get<T>(prefab);

		if(prepare)
			obj.Prepare(GetResetId ());

		return obj;
	}

	public static ulong GetResetId () {
		if (!HasInstance) Create();

		unchecked {
			return ++Instance.resetIdCounter;
		}
	}

	public static void CreatePool<T> (T prefab, int poolCount)
		where T : class, IPoolableObject
	{
		if (prefab == null || poolCount <= 0) return;
		if (!HasInstance) Create();

		var tmp = new T[poolCount];

		for(int i = 0; i < poolCount; ++i) {
			tmp[i] = Instance._Get<T>(prefab);
			tmp[i].Prepare(0);
		}

		for(int i = 0; i < poolCount; ++i) {
			tmp[i].ReturnToPool();
		}
	}
	
	T _Get<T> (T prefab)
		where T : class, IPoolableObject
	{
		if(dic == null)		ConstuctCollection();

        if(!(prefab is Component)) {
            Debug.LogError(typeof(T).Name + " must be a Component to be reset.");
            return null;
        }
		
		if(!dic.ContainsKey(prefab))
			dic.Add(prefab, new List<IPoolableObject>());
		
		var list = dic[prefab];
		T obj = default(T);
		
		for(int i = 0; i < list.Count; ++i) {
			
			if(!list[i].IsActive) {
				obj = list[i] as T;
				break;
			}
		}
		
		if(obj == null) {
            var prefrabComponent = prefab as Component;

            var com = GameObject.Instantiate(prefrabComponent);
            com.transform.SetParent(this.transform);

			com.name = string.Format("{0} ({1})", prefrabComponent.name, counter);

			++counter;

            obj = com as T;

            obj.SetPrefab(prefab);
            list.Add(obj);
		}
		
		return obj;
	}
}
