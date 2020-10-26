using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidRemover : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private GameObject player;

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            asteroidSpawner.asteroids
                .Where(i => i.gameObject.activeSelf == true)
                .OrderBy(i => Vector3.Distance(i.transform.position, player.transform.position))
                .FirstOrDefault().SetActive(false);
        }
    }
}
