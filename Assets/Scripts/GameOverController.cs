using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Texto para exibir a pontuação
    public TextMeshProUGUI highScoreText; // Texto para exibir a pontuação máxima
    public TextMeshProUGUI newHighScore; // Texto para exibir o contador de minerais

    void Start()
    {
        scoreText.text = PlayerPrefs.GetFloat("Score").ToString();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text = PlayerPrefs.GetFloat("HighScore").ToString();
        }
        else
        {
            highScoreText.text = PlayerPrefs.GetFloat("Score").ToString();
        }
        if (PlayerPrefs.GetFloat("Score") > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
            newHighScore.text = "New High Score!";
        }
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Home");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameView");
    }

    public void watchAd()
    {
        // Implement
    }
}
