using UnityEngine;

public class BallMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartBallMovement(Rigidbody2D rb)
    {
        //Randomize the initial direction of the ball
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;
        rb.linearVelocity = randomDirection * 5f; // Adjust the speed as needed
        
        
       
    }


}
