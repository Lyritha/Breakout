using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class BallBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private bool gameStarted = false;
    private float offsetY = 0.5f;

    [SerializeField] private Transform paddle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddle = FindAnyObjectByType<PaddleController>().transform;
    }

    private void Update()
    {
        StartingPhase();
    }

    private void StartingPhase()
    {
        if (!gameStarted)
        {
            transform.position = paddle.position + new Vector3(0f, offsetY, 0f);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true;
                StartBallMovement(rb);
            }
        }
    }

    private void Reset()
    {
        gameStarted = false;
        rb.linearVelocity = Vector2.zero;
    }

    private void StartBallMovement(Rigidbody2D rb)
    {
        float randomX = Random.Range(-0.8f, 0.8f);
        Vector2 direction = new Vector2(randomX, 1f).normalized;

        rb.linearVelocity = direction * 5f;
        currentVelocity = rb.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = currentVelocity.magnitude;

        if (collision.gameObject.TryGetComponent(out PaddleController paddleController))
        {
            Vector2 collisionPoint = collision.GetContact(0).point;
            Vector2 paddleCenter = paddleController.transform.position;

            float offsetX = collisionPoint.x - paddleCenter.x;
            float normalizedOffsetX = offsetX / (paddleController.GetComponent<BoxCollider2D>().bounds.size.x * 0.5f);
            normalizedOffsetX = Mathf.Clamp(normalizedOffsetX, -0.8f, 0.8f);

            Vector2 newDirection = new Vector2(normalizedOffsetX, 1f).normalized;

            rb.linearVelocity = newDirection * speed;
            currentVelocity = rb.linearVelocity;
            return;
        }

        ContactPoint2D contact = collision.GetContact(0);
        Vector2 reflectDirection = Vector2.Reflect(currentVelocity.normalized, contact.normal);

        // Fix: voorkom dat de bal horizontaal blijft bewegen
        if (Mathf.Abs(reflectDirection.y) < 0.25f)
        {
            reflectDirection.y = reflectDirection.y < 0 ? -0.25f : 0.25f;
            reflectDirection = reflectDirection.normalized;
        }

        rb.linearVelocity = reflectDirection * speed;
        currentVelocity = rb.linearVelocity;
    }
    private void OnEnable()
    {
        GameEvents.onBallLost += Reset;
    }

    private void OnDisable()
    {
        GameEvents.onBallLost -= Reset;
    }
}