using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Asteroid" || other.gameObject.tag == "Boss")
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayerDeath();
            }

            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene");
        }
        else if (other.gameObject.tag == "Mineral")
        {
            AudioManager.instance.PlayCollect();
            Destroy(other.gameObject);
            IRocket rocket = gameObject.GetComponent<IRocket>();
            if (rocket != null)
            {
                rocket.MineralCount += 2;
            }
        }
    }
}
