using System;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    BallMovement ballMovement;
    private Vector2 currentVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballMovement = GetComponent<BallMovement>();
        ballMovement.StartBallMovement(rb);
        currentVelocity = rb.linearVelocity;
    }

    private void Update()
    {
        //draw a normal from the rigidbody
        Debug.DrawRay(transform.position, rb.linearVelocity.normalized, Color.red);
   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TryGetComponent(out PaddleController paddleController))
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 reflectDirection = Vector2.Reflect(currentVelocity.normalized, contact.normal);
            rb.linearVelocity = reflectDirection * currentVelocity.magnitude;
            currentVelocity = rb.linearVelocity;
        }
    }
}
