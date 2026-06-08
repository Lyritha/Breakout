using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public static event Action LevelRequested;
    public static event Action<LevelDefinition> LevelChanged;

    [SerializeField]
    private LevelList levels;
    [SerializeField]
    private LevelDefinition currentLevel;
    [SerializeField]
    private int currentLevelIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool LoadNextLevel()
    {
        int nextIndex = currentLevelIndex + 1;
        if (nextIndex >= levels.levels.Length)
        {
            Debug.Log("No more levels to load.");
            return false;
        }
        LoadLevel(nextIndex);
        return true;
    }

    public void LoadLevel(int index)
    {
        if (index < 0 || index >= levels.levels.Length)
        {
            Debug.LogError("Invalid level index: " + index);
            return;
        }

        currentLevelIndex = index;
        currentLevel = levels.levels[currentLevelIndex];

        LevelChanged?.Invoke(currentLevel);
    }

    public bool HasNextLevel()
    {
        bool hasNextLevel = currentLevelIndex + 1 < levels.levels.Length;

        // only call this if there are other levels, otherwise it will be called on the last level and cause issues with the next level screen
        if (hasNextLevel) LevelRequested?.Invoke();

        return hasNextLevel;
    }



    public LevelDefinition CurrentLevel => currentLevel;
    public LevelDefinition[] Levels => levels.levels;
}
