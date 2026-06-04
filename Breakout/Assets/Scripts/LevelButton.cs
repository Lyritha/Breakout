using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Image background;
    [SerializeField]
    private Button button;

    private LevelDefinition level;


    public void SetLevel(LevelDefinition level)
    {
        this.level = level;
        text.text = level.levelName;
        background.color = level.levelColor;

        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log($"Selected level: {level.name}");
    }
}
