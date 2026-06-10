using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup group;

    private void Awake()
    {
        CloseMenu();

        if (LevelManager.Instance != null) LevelManager.LevelRequested += OpenMenu;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
        if (LevelManager.Instance != null) LevelManager.LevelRequested -= OpenMenu;
    }

    public void NextLevel()
    {
        if (LevelManager.Instance == null) return;
        Score.AddMult(0.05f);

        CloseMenu();
        GameEvents.onBallReset?.Invoke();
    }

    public void MainMenu()
    {
        CloseMenu();
        SceneManager.LoadScene(0);
    }

    private void OpenMenu()
    {
        LevelManager.Instance.LoadNextLevel();

        Cursor.visible = true;
        Time.timeScale = 0;

        if (group == null) return;

        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    private void CloseMenu()
    {
        Time.timeScale = 1;

        if (group == null) return;

        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}
