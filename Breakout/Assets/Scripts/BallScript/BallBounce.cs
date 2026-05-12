using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartBallMovement(rb);
        currentVelocity = rb.linearVelocity;
    }

    

    public void StartBallMovement(Rigidbody2D rb)
    {
        //Randomize the initial direction of the ball
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;
        rb.linearVelocity = randomDirection * 5f; // Adjust the speed as needed
    }
    private void Update()
    {
        //draw a normal from the rigidbody
        Debug.DrawRay(transform.position, rb.linearVelocity.normalized, Color.red);
   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //try get paddle controller 
        if (collision.gameObject.TryGetComponent<PaddleController>(out PaddleController paddleController))
        {
           
                Vector2 collisionPoint = collision.GetContact(0).point;
                Vector2 paddleCenter = paddleController.transform.position;
                float offsetX = collisionPoint.x - paddleCenter.x;
                float normalizedOffsetX = offsetX / (paddleController.GetComponent<BoxCollider2D>().size.x * 0.5f);
                
           
                Vector2 newDirection = new Vector2(normalizedOffsetX, 1f).normalized; 
                rb.linearVelocity = newDirection * currentVelocity.magnitude; 
                currentVelocity = rb.linearVelocity; 
            
            return;
        }
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 reflectDirection = Vector2.Reflect(currentVelocity.normalized, contact.normal);
            rb.linearVelocity = reflectDirection * currentVelocity.magnitude;
            currentVelocity = rb.linearVelocity;
    }
}
