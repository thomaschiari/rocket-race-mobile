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
        float score = PlayerPrefs.GetFloat("Score", 0);
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);

        scoreText.text = score.ToString();

        if (score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            highScore = score;
            newHighScore.text = "New High Score!";
        }

        highScoreText.text = highScore.ToString();
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
