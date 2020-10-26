using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum AsteroidFlags
{
	Default = 0,
	ChangeRotation = 1,
	ChangeColor = 2,
	ChangeScale = 4,

}

public class AsteroidScript : MonoBehaviour
{
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float destroyDistance;

	[SerializeField] private AsteroidFlags rotationFlag;
	[SerializeField] private AsteroidFlags colorFlag;
	[SerializeField] private AsteroidFlags scaleFlag;

	private GameObject player;
	private AsteroidSpawner asteroidSpawner;
	private int rotationDirection = 1;

    private void Awake()
    {
		var flags = rotationFlag | colorFlag | scaleFlag;

		rotationDirection = flags.HasFlag(AsteroidFlags.ChangeRotation) ? -1 : 1;
		GetComponent<Renderer>().material.color =
			flags.HasFlag(AsteroidFlags.ChangeColor) ?
			Color.red :
			GetComponent<Renderer>().material.color;
		transform.localScale = flags.HasFlag(AsteroidFlags.ChangeScale) ? transform.localScale * 0.5f : transform.localScale;
	}

    private void Start()
	{
		player = FindObjectOfType<PlayerScript>().gameObject;
		asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
	}

	// Update is called once per frame
	void Update()
	{
		//Destroing asteroid when it out of distance
		gameObject.SetActive(!(transform.position.z - player.transform.position.z < -destroyDistance));

		//Rotating asteroid
		transform.Rotate(rotationDirection * Vector3.forward * rotationSpeed * Time.deltaTime);
	}
}
