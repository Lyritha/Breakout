using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Image background;
    [SerializeField]
    private Button button;

    private int levelIndex;

    public void SetLevel(int index)
    {
        this.levelIndex = index;
        LevelDefinition level = LevelManager.Instance.Levels[index];

        text.text = level.levelName;
        background.color = level.levelColor;

        button.onClick.AddListener(OnClick);

        if (!LevelManager.Instance.IsLevelUnlocked(index)) button.interactable = false;
    }

    public void OnClick()
    {
        LevelManager.Instance.LoadLevel(levelIndex);
        SceneManager.LoadScene(1);
    }
}
