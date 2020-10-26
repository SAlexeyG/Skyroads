using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
	[SerializeField] private float interval;
	[SerializeField] private GameObject asteroid;

	public Queue<GameObject> asteroids = new Queue<GameObject>();

	//Creating asteroids
	private IEnumerator Start()
	{
		ObjectsPool.Instance.PrepareObjcets(asteroid, 5);
		while (true)
		{
			yield return new WaitForSeconds(interval);
			var obj = ObjectsPool.Instance.GetObject(asteroid);
			obj.transform.position = new Vector3(Random.Range(-4f, 4f), 1, transform.position.z);
			asteroids.Enqueue(obj);
		}
	}
}
