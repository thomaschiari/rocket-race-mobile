using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button startButton;

    void Awake()
    {
        PlayerPrefs.SetInt("GameMode", 0);
        PlayerPrefs.SetInt("StoryMode", 0);
    }

    void Start()
    {
        UpdateStartButtonState();
    }

    void Update()
    {
        UpdateStartButtonState();
    }
    public void StartGame()
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

    public void ChooseShip()
    {
        SceneManager.LoadScene("ChooseShip");
    }

    public void Rules()
    {
        SceneManager.LoadScene("Rules");
    }

    // public void Rules()
    // {
    //     SceneManager.LoadScene("Rules");
    // }

    public void SelectEndlessMode()
    {
        PlayerPrefs.SetInt("GameMode", 1);
        PlayerPrefs.SetInt("StoryMode", 0);
    }

    public void SelectStoryMode()
    {
        PlayerPrefs.SetInt("StoryMode", 1);
        PlayerPrefs.SetInt("GameMode", 0);
    }

    private void UpdateStartButtonState()
    {
        if (PlayerPrefs.GetInt("GameMode") == 0 && PlayerPrefs.GetInt("StoryMode") == 0)
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

}
