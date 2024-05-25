using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameView");
    }

    public void ChooseShip()
    {
        SceneManager.LoadScene("ChooseShip");
    }

    // public void Rules()
    // {
    //     SceneManager.LoadScene("Rules");
    // }
}
