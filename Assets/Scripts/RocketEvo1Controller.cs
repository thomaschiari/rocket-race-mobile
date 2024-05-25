using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketEvo1Controller : MonoBehaviour, IRocket
{
    public float speed = 150f; // Velocidade do foguete
    public GameObject projectilePrefab; // Prefab do projétil
    public Transform firePoint; // Ponto de origem do disparo
    public static int mineralCount; // Contador de minerais
    public TextMeshProUGUI mineralCountText; // Texto para exibir o contador de minerais
    public TextMeshProUGUI scoreText; // Texto para exibir a pontuação
    private float minX, maxX, minY, maxY;
    private Vector2 movement = Vector2.zero; // Armazena o movimento
    private Rigidbody2D rb;
    private float score; // Variável para armazenar a pontuação do jogador
    private float startTime; // Variável para armazenar o tempo inicial

    public int MineralCount
    {
        get { return mineralCount; }
        set { mineralCount = value; UpdateMineralCount(); }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Calcular os limites da tela
        Camera camera = Camera.main;
        Vector3 screenBottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 screenTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;
        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;

        startTime = Time.time;

        score = 0f;
        mineralCount = 2;

        UpdateMineralCount();
        UpdateScore();
    }

    void Update()
    {
        // Aplicar movimento
        Vector2 move = movement * speed * Time.deltaTime;
        rb.velocity = move;

        // Log do movimento aplicado
        Debug.Log("Moving: " + move);

        // Restringir a posição do foguete dentro dos limites da tela
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;

        UpdateScore();
        UpdateMineralCount();
    }

    public void MoveUp()
    {
        movement = Vector2.up;
        Debug.Log("MoveUp");
    }

    public void MoveDown()
    {
        movement = Vector2.down;
        Debug.Log("MoveDown");
    }

    public void MoveLeft()
    {
        movement = Vector2.left;
        Debug.Log("MoveLeft");
    }

    public void MoveRight()
    {
        movement = Vector2.right;
        Debug.Log("MoveRight");
    }

    public void StopMovement()
    {
        movement = Vector2.zero;
        Debug.Log("StopMovement");
    }

    public void Fire()
    {
        if (mineralCount > 0)
        {
            FireProjectiles();
            mineralCount--;
        }
    }

    void FireProjectiles()
    {
        // Disparar o projétil principal
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Calcular as direções dos projéteis diagonais
        float angleOffset = 15f; // Ângulo dos projéteis diagonais
        Quaternion leftRotation = Quaternion.Euler(0, 0, angleOffset);
        Quaternion rightRotation = Quaternion.Euler(0, 0, -angleOffset);

        // Disparar os projéteis diagonais
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * leftRotation);
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * rightRotation);
    }

    void UpdateMineralCount()
    {
        // Atualizar o texto para mostrar a quantidade de tiros restantes
        mineralCountText.text = "Shots: " + mineralCount;
    }

    void UpdateScore()
    {
        // Calcular a pontuação baseada no tempo passado e nos minerais coletados
        score = Mathf.Round((Time.time - startTime + mineralCount) * 100) / 100;
        scoreText.text = "Score: " + score;
        PlayerPrefs.SetFloat("Score", score);
    }

    public float GetScore()
    {
        return Mathf.Round(score * 100) / 100;
    }
}
