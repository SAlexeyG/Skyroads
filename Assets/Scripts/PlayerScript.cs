using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float sensitivity;
	[SerializeField] private float rotationDegree;

	[SerializeField] private GameObject playerGraphics;
	[SerializeField] private SmoothFollow camera;

	public event Action OnAsteroidSmash;
	public event Action OnAsteroidPass;

	Vector3 mov;
	Quaternion rot;
	float boost = 1f;
	float orderCameraDistance;
	float orderCameraHeight;

	public float Boost { get => boost; }
	public float Speed { get => speed; set => speed = value; }

	private void Start()
	{
		//Saving initially camera distance and height
		orderCameraDistance = camera.distance;
		orderCameraHeight = camera.height;
	}

	// Update is called once per frame
	void Update()
	{
		mov = Vector3.forward;
		rot = Quaternion.identity;

		//Moving left
		if (Input.GetAxis("Horizontal") < 0)
		{
			mov.x -= sensitivity;
			rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rotationDegree);
		}

		//Moving right
		if (Input.GetAxis("Horizontal") > 0)
		{
			mov.x += sensitivity;
			rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, -rotationDegree);
		}

		//Moving fast
		if (Input.GetKey(KeyCode.Space))
		{
			//Smoothly zooming
			camera.distance = Mathf.Lerp(camera.distance, orderCameraDistance * 0.5f, 0.1f);
			camera.height = Mathf.Lerp(camera.height, orderCameraHeight * 0.5f, 0.1f);

			boost = 2f;
		}
		else
		{
			camera.distance = Mathf.Lerp(camera.distance, orderCameraDistance, 0.1f);
			camera.height = Mathf.Lerp(camera.height, orderCameraHeight, 0.1f);

			boost = 1f;
		}

		//Making tilt
		playerGraphics.transform.rotation = Quaternion.Lerp(playerGraphics.transform.rotation, rot, 0.1f);

		//Moving
		transform.Translate(mov * speed * boost * Time.deltaTime);
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4f, 4f), transform.position.y, transform.position.z);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<AsteroidScript>())
			OnAsteroidSmash?.Invoke();
	}
}
