using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainScreen;
    [SerializeField]
    private RectTransform levelScreen;

    private void Awake() => ShowMain();

    public void ShowMain()
    {
        mainScreen.gameObject.SetActive(true);
        levelScreen.gameObject.SetActive(false);
    }

    public void ShowLevels()
    {
        mainScreen.gameObject.SetActive(false);
        levelScreen.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
