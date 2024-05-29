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
    public TextMeshProUGUI watchAdButtonText; // Texto para exibir no botão de assistir ao anúncio

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
        // Inscrever-se no evento de anúncio carregado
        AdManager.OnAdLoaded += OnAdLoaded;

        // Verificar o estado inicial do anúncio
        UpdateAdButtonState();
        UpdateMineralCountText();
    }

    private void Update()
    {
        UpdateMineralCountText();
        UpdateAdButtonState();
    }

    private void OnDestroy()
    {
        // Desinscrever-se dos eventos de anúncio assistido e carregado
        AdManager.OnAdWatched -= OnAdWatched;
        AdManager.OnAdLoaded -= OnAdLoaded;
    }

    public void backToMenu()
    {
        AudioManager.instance.PlayMenuMusic();
        SceneManager.LoadScene("Home");
    }

    public void RestartGame()
    {
        AudioManager.instance.FromMenuToGame();
        if (PlayerPrefs.GetInt("GameMode") == 1)
        {
            SceneManager.LoadScene("GameView");
        }
        else if (PlayerPrefs.GetInt("StoryMode") == 1)
        {
            SceneManager.LoadScene("StoryGameView");
        }
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
        watchAdButtonText.text = "Ad watched!";
        PlayerPrefs.SetInt("AdWatched", 1);
    }

    private void OnAdLoaded()
    {
        // Habilitar o botão de assistir ao anúncio quando um anúncio for carregado
        watchAdButton.interactable = true;
        watchAdButtonText.text = "Watch Ad :)";
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
            watchAdButtonText.text = "Ad watched!";
        }
        else {
            AdManager adManager = FindObjectOfType<AdManager>();
            if (adManager != null && adManager.IsAdLoaded())
            {
                watchAdButton.interactable = true;
                watchAdButtonText.text = "Watch Ad :)";
            }
            else
            {
                watchAdButton.interactable = false;
                watchAdButtonText.text = "No Ads Available";
            }
        }
    }
}
