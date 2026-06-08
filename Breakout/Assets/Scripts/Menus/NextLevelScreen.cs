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

        if (LevelManager.Instance != null)
        {
            LevelManager.LevelRequested += ShowMenu;
        }
    }

    private void ShowMenu()
    {
        OpenMenu();
    }

    public void NextLevel()
    {
        if (LevelManager.Instance == null) return;

        Time.timeScale = 1;
        CloseMenu();

        LevelManager.Instance.LoadNextLevel();
    }

    public void MainMenu()
    {
        CloseMenu();
        SceneManager.LoadScene(0);
    }

    private void OpenMenu()
    {
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




    private void OnDestroy()
    {
        Time.timeScale = 1;
        if (LevelManager.Instance != null) LevelManager.LevelRequested -= ShowMenu;
    }
}
