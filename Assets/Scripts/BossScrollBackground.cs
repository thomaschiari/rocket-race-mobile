using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScrollBackground : MonoBehaviour
{
    private float speed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
