using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Texto para exibir a pontuação
    public TextMeshProUGUI highScoreText; // Texto para exibir a pontuação máxima
    public TextMeshProUGUI newHighScore; // Texto para exibir a nova pontuação máxima
    public TextMeshProUGUI textMineralCount; // Texto para exibir o contador de minerais
    public Button watchAdButton; // Botão para assistir ao anúncio

    void Start()
    {
        float score = PlayerPrefs.GetFloat("Score", 0);
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);

        PlayerPrefs.SetInt("AdWatched", 0);

        int mineralCount = (int)(score / 10);

        if (PlayerPrefs.HasKey("MineralCount"))
        {
            mineralCount += PlayerPrefs.GetInt("MineralCount");
        }

        PlayerPrefs.SetInt("MineralCount", mineralCount);

        scoreText.text = score.ToString();

        if (score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            highScore = score;
            newHighScore.text = "New High Score!";
        }

        highScoreText.text = highScore.ToString();

        textMineralCount.text = "Minerals: " + mineralCount;

        // Adicionar listener ao botão de assistir ao anúncio
        watchAdButton.onClick.AddListener(() => ShowAd());

        // Inscrever-se no evento de anúncio assistido
        AdManager.OnAdWatched += OnAdWatched;
    }

    private void Update()
    {
        UpdateMineralCountText();
        UpdateAdButtonState();
    }

    private void OnDestroy()
    {
        // Desinscrever-se do evento de anúncio assistido
        AdManager.OnAdWatched -= OnAdWatched;
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Home");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameView");
    }

    private void ShowAd()
    {
        // Chamar o AdManager para mostrar o anúncio
        AdManager adManager = FindObjectOfType<AdManager>();
        if (adManager != null)
        {
            adManager.ShowAd();
        }
    }

    private void OnAdWatched()
    {
        // Aumentar a quantidade de minerais após assistir ao anúncio
        int minerals = PlayerPrefs.GetInt("MineralCount", 0);
        PlayerPrefs.SetInt("MineralCount", minerals + 10); // Recompensa de 10 minerais por anúncio assistido
        PlayerPrefs.Save();

        // Atualizar o texto do contador de minerais
        UpdateMineralCountText();
        // Atualizar a visibilidade do botão de assistir ao anúncio
        watchAdButton.interactable = false;
        // Atualizar texto do botão de assistir ao anúncio
        watchAdButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ad watched!";
        PlayerPrefs.SetInt("AdWatched", 1);
    }

    private void UpdateMineralCountText()
    {
        int minerals = PlayerPrefs.GetInt("MineralCount", 0);
        textMineralCount.text = "Minerals: " + minerals;
    }

    private void UpdateAdButtonState()
    {
        if (PlayerPrefs.GetInt("AdWatched", 0) == 1)
        {
            watchAdButton.interactable = false;
            watchAdButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ad watched!";
        }
    }
}
