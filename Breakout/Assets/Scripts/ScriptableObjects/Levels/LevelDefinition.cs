using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level Definition")]
public class LevelDefinition : ScriptableObject
{
    public LevelType levelType = LevelType.Default;

    public string levelName = "New Level";
    public Color levelColor = Color.white;

    public LevelRow[] rows = new LevelRow[10];
}

public enum LevelType
{
    Default,
    Boss
}