using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float acceleration;

    [SerializeField] private PlayerScript player;
    [SerializeField] private AsteroidScript asteroid;
    [SerializeField] private AsteroidSpawner asteroidSpawner;

    [SerializeField] private GameObject gameStartText;
    [SerializeField] private GamePlayingScript gamePlayingScript;
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] private AudioSource audioSource;

    enum GameState
    {
        Start,
        Playing,
        End,

    }

    GameState gameState = GameState.Start;
    event Action OnScoreChange;

    Score score = new Score("0");
    public Score Score
    {
        get => score;
        set
        {
            score = value;
            OnScoreChange?.Invoke();
        }
    }

    int highscore = 0;
    int asteroids = 0;
    float stayingTime = 0;
    float time = 0f;

    void GameOver() => gameState = GameState.End;

    private void Start()
    {
        //Getting saved highscore
        highscore = PlayerPrefs.GetInt("Score");
    }

    private void Awake()
    {
        player.OnAsteroidSmash += delegate {
            audioSource.Play();
            GameOver();
        };

        OnScoreChange += delegate
        {
            gamePlayingScript.SetUIValues(
                    GetPrivateField("value", score).ToGUIString(),
                    ((score < highscore) ? highscore : (int)score).ToString(),
                    ConvertTime((int)(Time.time - stayingTime)),
                    asteroids.ToString()
                    );
        };
    }

    private void OnDestroy()
    {
        player.OnAsteroidSmash -= delegate {
            audioSource.Play();
            GameOver();
        };

        OnScoreChange -= delegate
        {
            gamePlayingScript.SetUIValues(
                    GetPrivateField("value", score).ToGUIString(),
                    ((score < highscore) ? highscore : (int)score).ToString(),
                    ConvertTime((int)(Time.time - stayingTime)),
                    asteroids.ToString()
                    );
        };
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:

                //Pause
                Time.timeScale = 0;

                //Show text "Press any key..."
                gameStartText.SetActive(true);
                if (Input.anyKeyDown)
                {
                    gameState = GameState.Playing;
                    Time.timeScale = 1;

                    //Save start time
                    stayingTime = Time.time;
                    gameStartText.SetActive(false);
                }
                break;

            case GameState.Playing:

                //Show GUI
                gamePlayingScript.gameObject.SetActive(true);

                if (asteroidSpawner.asteroids.Count > 0)
                    if (asteroidSpawner.asteroids.Peek().transform.position.z - player.transform.position.z < -1f)
                    {
                        Score += 5;
                        asteroids++;
                        asteroidSpawner.asteroids.Dequeue();
                    }

                time += Time.deltaTime;
                if (time >= 1f)
                {
                    player.Speed += acceleration;
                    Score += (player.Boost > 1f) ? 2 : 1;
                    time--;
                }
                break;

            case GameState.End:

                //Pause
                Time.timeScale = 0;

                gamePlayingScript.gameObject.SetActive(false);
                gameOverScript.gameObject.SetActive(true);

                //Save highscore
                PlayerPrefs.SetInt("Score", (Score < highscore) ? highscore : (int)Score);

                //Show stats
                gameOverScript.SetUIValues(
                    Score > highscore,
                    "Score: " + Score.ToString(),
                    "Time: " + ConvertTime((int)(Time.time - stayingTime)),
                    "Asteroids: " + asteroids.ToString()
                    );
                break;
        }
    }

    int GetPrivateField(string field, Score instance)
    {
        Type type = instance.GetType();
        FieldInfo fieldInfo = type.GetField(field, BindingFlags.NonPublic | BindingFlags.Instance);
        return (int)fieldInfo.GetValue(instance);
    }

    string ConvertTime(int time) => time / 60 + ":" + time % 60;
}
