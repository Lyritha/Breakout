using UnityEngine;

public class OnGameOverTempScript : MonoBehaviour
{
    private GameManager gameManager;
    
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    private void OnEnable()
    {
        GameEvents.onGameOver += gameManager.GameOver;
    }
    
    private void OnDisable()
    {
        GameEvents.onGameOver -= gameManager.GameOver;
    }
    
}
