using TMPro;
using UnityEngine;

// Manages the player's score and updates the UI accordingly
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    private int score;

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

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText() => scoreText.text = $"Score: {NumberFormatter.FormatNumber(score)}";
}


// safe way to add score without needing to reference the ScoreManager instance directly
public static class Score
{
    public static void Add(int amount)
    {

        if (ScoreManager.Instance == null)
            return;

        ScoreManager.Instance.AddScore(amount);
    }

    public static void Reset()
    {
        if (ScoreManager.Instance == null)
            return;

        ScoreManager.Instance.ResetScore();
    }
}