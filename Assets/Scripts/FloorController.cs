using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private float destroyDistance;

    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject player;

    Queue<GameObject> road = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        ObjectsPool.Instance.PrepareObjcets(floorPrefab, count);

        for(int i = 0; i < count; i++)
		{
            var obj = ObjectsPool.Instance.GetObject(floorPrefab);
            obj.transform.position = new Vector3(0, 0, i * 10);
            road.Enqueue(obj);
		}
    }

    // Update is called once per frame
    void Update()
    {
        var floorPosition = road.Peek().transform.position.z;

        if (floorPosition - player.transform.position.z < -destroyDistance)
        {
            road.Dequeue().SetActive(false);
            var obj = ObjectsPool.Instance.GetObject(floorPrefab);
            obj.transform.position = new Vector3(0, 0, floorPosition + count * 10);
            road.Enqueue(obj);
        }
    }
}
