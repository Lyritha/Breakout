using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_Text score;

    [SerializeField]
    private CanvasGroup group;

    private void Awake()
    {
        if (group != null)
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }

        GameEvents.onGameWon += () => ShowScreen(true);
        GameEvents.onGameOver += () => ShowScreen(false);
    }

    private void OnDisable()
    {
        GameEvents.onGameWon -= () => ShowScreen(true);
        GameEvents.onGameOver -= () => ShowScreen(false);
    }

    public void ShowScreen(bool hasWon)
    {
        Cursor.visible = true;
        Time.timeScale = 0;

        if (group != null)
        {
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }

        title.text = hasWon ? "You won!" : "You lost...";
        
        if (ScoreManager.Instance != null)
        {
            score.text = $"Score: {ScoreManager.Instance.Score}";
        }
    }

    public void Retry() => LoadScene(1);
    public void MainMenu() => LoadScene(0);


    private void LoadScene(int scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
}
