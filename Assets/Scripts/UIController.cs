using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI mineralCountText; // Texto para exibir o contador de minerais
    public TextMeshProUGUI scoreText; // Texto para exibir a pontuação

    void OnEnable()
    {
        RocketController.OnMineralCountChanged += UpdateMineralCount;
        RocketController.OnScoreChanged += UpdateScore;
    }

    void OnDisable()
    {
        RocketController.OnMineralCountChanged -= UpdateMineralCount;
        RocketController.OnScoreChanged -= UpdateScore;
    }

    void UpdateMineralCount(int count)
    {
        mineralCountText.text = "Shots: " + count;
    }

    void UpdateScore(float score)
    {
        scoreText.text = "Score: " + Mathf.Round(score * 100) / 100;
    }
}
