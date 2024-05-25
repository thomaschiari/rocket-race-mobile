using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Texto para exibir a pontuação
    private RocketController rocketController;

    void Start()
    {
        rocketController = FindObjectOfType<RocketController>();
        scoreText.text = rocketController.GetScore().ToString();
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
