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

	[SerializeField] private Slider musicSlider;
	[SerializeField] private AudioSource music;

	[SerializeField] private Slider soundsSlider;
	[SerializeField] private AudioSource[] sounds;

	[SerializeField] private AudioSource click;

	public void SetUIValues(string score, string highscore, string time, string asteroids)
	{
		this.score.text = score;
		this.highscore.text = highscore;
		this.time.text = time;
		this.asteroids.text = asteroids;
	}

	public void SetMusicVolume()
    {
		music.volume = musicSlider.value;
    }

	public void SetSoundsVolume()
    {
		foreach (var source in sounds)
			source.volume = soundsSlider.value;
    }

	public void ButtonClick()
    {
		click.Play();
    }
}
