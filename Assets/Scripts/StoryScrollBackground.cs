using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScrollBackground : MonoBehaviour
{
    private float speed = StoryGameController.speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
