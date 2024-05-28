using System;
using UnityEngine;

public abstract class BaseRocketController : MonoBehaviour, IRocket
{
    public static event Action<int> OnMineralCountChanged; // Evento para mudança de mineralCount
    public static event Action<float> OnScoreChanged; // Evento para mudança de score
    private int mineralCount;
    private float score;
    private float startTime;
    private float multiplier;
    public float Multiplier
    {
        get { return multiplier; }
        set { multiplier = value; }
    }
    public int MineralCount
    {
        get { return mineralCount; }
        set
        {
            mineralCount = value;
            OnMineralCountChanged?.Invoke(mineralCount);
            UpdateScore();
        }
    }

    protected virtual void Start()
    {
        startTime = Time.time;
        score = 0f;
        MineralCount = 5; // Valor inicial, pode ser ajustado
        multiplier = 1f;
        Debug.Log("Multiplier: " + multiplier);
    }

    protected virtual void Update()
    {
        UpdateScore();
    }

    protected void UpdateScore()
    {
        score = Mathf.Round(((Time.time - startTime + mineralCount) * multiplier) * 100) / 100;
        OnScoreChanged?.Invoke(score);
        PlayerPrefs.SetFloat("Score", score);
    }
}
