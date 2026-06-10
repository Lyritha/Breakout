using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform parent;
    [SerializeField]
    private LevelButton levelButtonPrefab;

    private void Start()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError("LevelManager instance is not initialized.");
            return;
        }

        for (int i = 0; i < LevelManager.Instance.Levels.Length; i++)
        {
            LevelButton button = Instantiate(levelButtonPrefab, parent);
            button.SetLevel(i);
        }
    }
}
