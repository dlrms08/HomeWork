using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager intance;
    public Text scoreText;
    private int Score;

    private void Awake()
    {
        intance = this;
    }

    private void Start()
    {
        Score = 0;
    }

    public void AddScore(int vol)
    {
        Score += vol;

        scoreText.text = Score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
