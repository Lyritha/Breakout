using TMPro;
using UnityEngine;

// Manages the player's score and updates the UI accordingly
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text multText;
    private int score;
    private float multiplier = 1;

    public int Score => score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void Start() => UpdateScoreText();

    public void IncreaseMult(float amount)
    {
        multiplier += amount;
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += Mathf.RoundToInt(amount * multiplier);
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {NumberFormatter.FormatNumber(score)}";
        multText.text = $"{multiplier}X";
    }
}


// safe way to add score without needing to reference the ScoreManager instance directly
public static class Score
{
    public static void AddMult(float amount)
    {
        if (ScoreManager.Instance == null) return;
        ScoreManager.Instance.IncreaseMult(amount);
    }

    public static void Add(int amount)
    {
        if (ScoreManager.Instance == null) return;
        ScoreManager.Instance.AddScore(amount);
    }

    public static void Reset()
    {
        if (ScoreManager.Instance == null) return;
        ScoreManager.Instance.ResetScore();
    }
}