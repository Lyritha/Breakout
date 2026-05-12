using UnityEngine;

public class OnGameOverTempScript : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.onGameOver += GameOver;
    }
    
    private void OnDisable()
    {
        GameEvents.onGameOver -= GameOver;
    }

    private void GameOver()
    {
        //TODO: Implement game over screen  
            Debug.Log("Game Over!");
    }
}
