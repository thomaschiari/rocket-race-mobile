using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Home");
        }
        else if (other.gameObject.tag == "Mineral")
        {
            Destroy(other.gameObject);
            RocketController.mineralCount += 5;
        }
    }
}