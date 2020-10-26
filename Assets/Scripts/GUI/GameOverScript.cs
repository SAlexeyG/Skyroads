using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
	[SerializeField] private Text congratulations;
	[SerializeField] private Text score;
	[SerializeField] private Text asteroids;
	[SerializeField] private Text time;

	public void SetUIValues(bool congratulations, string score, string time, string asteroids)
	{
		this.congratulations.gameObject.SetActive(congratulations);
		this.score.text = score;
		this.time.text = time;
		this.asteroids.text = asteroids;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
