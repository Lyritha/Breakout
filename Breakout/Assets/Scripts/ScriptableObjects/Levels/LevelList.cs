using UnityEngine;

[CreateAssetMenu(fileName = "LevelList", menuName = "Levels/LevelList")]
public class LevelList : ScriptableObject
{
    public LevelDefinition[] levels;
}
