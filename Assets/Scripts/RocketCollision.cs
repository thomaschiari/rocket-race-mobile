using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Mineral")
        {
            Destroy(other.gameObject);
            RocketController.mineralCount += 5;
        }
    }
}
