using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class BallBounce : MonoBehaviour
{
    [SerializeField] private float startSpeed = 5f;
    [SerializeField] private float speedIncrement = 0.3f;
    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float minYFraction = 0.25f;
    [SerializeField] private float offsetY = 0.5f;

    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private float currentSpeed;
    private bool gameStarted = false;
    private Transform paddle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddle = FindAnyObjectByType<PaddleController>().transform;
        currentSpeed = startSpeed;
    }

    private void Update()
    {
        if (gameStarted) return;

        transform.position = paddle.position + new Vector3(0f, offsetY, 0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;
            StartBallMovement();
        }
    }

    private void Reset()
    {
        gameStarted = false;
        currentSpeed = startSpeed;
        rb.linearVelocity = Vector2.zero;
        transform.position = paddle.position + new Vector3(0f, offsetY, 0f);
    }

    private void StartBallMovement()
    {
        float randomX = Random.Range(-0.8f, 0.8f);
        Vector2 direction = ClampMinY(new Vector2(randomX, 1f).normalized);
        rb.linearVelocity = direction * currentSpeed;
        currentVelocity = rb.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PaddleController paddleController))
        {
            HandlePaddleCollision(collision, paddleController);
            return;
        }

        HandleSurfaceCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out PaddleController paddleController)) return;

        ContactPoint2D contact = collision.GetContact(0);
        if (contact.normal.y < 0.5f)
        {
            if (contact.point.y < paddleController.transform.position.y)
            {
                GameEvents.onBallLost?.Invoke();
                return;
            }

            Vector2 escapeDir = new Vector2(contact.normal.x, 0.5f).normalized;
            rb.linearVelocity = escapeDir * currentSpeed;
            currentVelocity = rb.linearVelocity;
        }
    }

    private void HandlePaddleCollision(Collision2D collision, PaddleController paddleController)
    {
        ContactPoint2D contact = collision.GetContact(0);

        if (contact.normal.y < 0.5f)
        {
            // Onderkant — bal verloren
            if (contact.point.y < paddleController.transform.position.y)
            {
                GameEvents.onBallLost?.Invoke();
                return;
            }

            // Zijkant — duw omhoog
            Vector2 escapeDir = new Vector2(contact.normal.x, 0.5f).normalized;
            rb.linearVelocity = escapeDir * currentSpeed;
            currentVelocity = rb.linearVelocity;
            return;
        }

        // Bovenkant
        currentSpeed = startSpeed;

        float offsetX = contact.point.x - paddleController.transform.position.x;
        float normalizedOffsetX = offsetX / (paddleController.GetComponent<BoxCollider2D>().size.x * 0.5f);
        normalizedOffsetX = Mathf.Clamp(normalizedOffsetX, -0.8f, 0.8f);

        Vector2 newDirection = ClampMinY(new Vector2(normalizedOffsetX, 1f).normalized);
        rb.linearVelocity = newDirection * currentSpeed;
        currentVelocity = rb.linearVelocity;
    }

    private void HandleSurfaceCollision(Collision2D collision)
    {
        currentSpeed = Mathf.Min(currentSpeed + speedIncrement, maxSpeed);

        Vector2 reflected = Vector2.Reflect(currentVelocity.normalized, collision.GetContact(0).normal);
        reflected = ClampMinY(reflected);
        rb.linearVelocity = reflected * currentSpeed;
        currentVelocity = rb.linearVelocity;
    }

    private Vector2 ClampMinY(Vector2 direction)
    {
        if (Mathf.Abs(direction.y) < minYFraction)
        {
            direction.y = minYFraction * (direction.y >= 0f ? 1f : -1f);
            direction = direction.normalized;
        }
        return direction;
    }

    private void OnEnable()
    {
        GameEvents.onBallReset += Reset;
        GameEvents.onBallLost += Reset;
    }

    private void OnDisable()
    {
        GameEvents.onBallReset -= Reset;
        GameEvents.onBallLost -= Reset;
    }
}