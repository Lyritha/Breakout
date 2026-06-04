using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level Definition")]
public class LevelDefinition : ScriptableObject
{
    public string levelName = "New Level";
    public Color levelColor = Color.white;

    public LevelRow[] rows;
}

