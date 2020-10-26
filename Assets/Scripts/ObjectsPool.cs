using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
	private static ObjectsPool instance;

	public static ObjectsPool Instance
	{
		get
		{
			if (instance != null) return instance;

			var obj = new GameObject("Objects Pool");
			instance = obj.AddComponent<ObjectsPool>();
			return instance;
		}
	}

	private Dictionary<GameObject, LinkedList<GameObject>> pool =
		new Dictionary<GameObject, LinkedList<GameObject>>();

	public GameObject GetObject(GameObject prefab)
	{
		if (!pool.ContainsKey(prefab))
		{
			var obj = Instantiate(prefab);
			pool[prefab] = new LinkedList<GameObject>();
			pool[prefab].AddLast(obj);
			return obj;
		}

		var objects = pool[prefab];
		foreach (var obj in objects)
			if (obj != null && !obj.activeSelf)
			{
				obj.SetActive(true);
				return obj;
			}

		var newObj = Instantiate(prefab);
		objects.AddLast(newObj);
		return newObj;
	}

	public void PrepareObjcets(GameObject prefab, int count)
	{
		if (pool.ContainsKey(prefab))
		{
			if (pool[prefab].Count >= count) return;

			var newObjectsCount = count - pool[prefab].Count;

			foreach (var gameObject in InstantiateObjects(prefab, newObjectsCount))
				pool[prefab].AddLast(gameObject);

			return;
		}

		pool[prefab] = new LinkedList<GameObject>();
		foreach (var gameObject in InstantiateObjects(prefab, count))
			pool[prefab].AddLast(gameObject);
	}

	private IEnumerable<GameObject> InstantiateObjects(GameObject prefab, int count)
	{
		var objects = new LinkedList<GameObject>();

		for (int i = 0; i < count; i++)
		{
			var obj = Instantiate(prefab);
			obj.SetActive(false);
			objects.AddLast(obj);
		}

		return objects;
	}
}
