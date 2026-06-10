using UnityEngine;

[System.Serializable]
public class LevelRow
{
    public Color color = Color.white;
    public int score = 10;
    public bool[] slots = new bool[9];
}

