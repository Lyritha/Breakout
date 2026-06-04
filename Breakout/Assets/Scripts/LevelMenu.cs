using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform parent;
    [SerializeField]
    private LevelButton levelButtonPrefab;


    [SerializeField]
    private LevelList levels;


    private void Start()
    {
        foreach (LevelDefinition level in levels.levels)
        {
            LevelButton button = Instantiate(levelButtonPrefab, parent);
            button.SetLevel(level);
        }
    }
}
