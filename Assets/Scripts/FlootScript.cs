using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlootScript : MonoBehaviour
{
	[SerializeField] private Animator animator;

	//Method for synchronizing animation
	private void Start()
	{
		animator.Play("FloorAnimation", -1, (Time.time - (int)Time.time / 2 * 2) / 2);
	}
}
