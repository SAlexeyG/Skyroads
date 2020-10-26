using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePlayingScript : MonoBehaviour
{
	[SerializeField] private Text score;
	[SerializeField] private Text highscore;
	[SerializeField] private Text time;
	[SerializeField] private Text asteroids;

	public void SetUIValues(string score, string highscore, string time, string asteroids)
	{
		this.score.text = score;
		this.highscore.text = highscore;
		this.time.text = time;
		this.asteroids.text = asteroids;
	}
}
