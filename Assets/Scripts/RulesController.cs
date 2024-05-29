using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesController : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Home");
    }
}
