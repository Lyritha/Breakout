using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SetMaxLevel()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 999);
    }
}
