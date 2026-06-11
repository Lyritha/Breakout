using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Vector4 bounds;
    private bool goingRight = true;
    private Vector2 moveDirection;

    private float timer;
    private float changeTime;

    public bool CanMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (Camera.main != null)
            bounds = ScreenWorldBounds.GetBounds(Camera.main);

        moveDirection = Vector2.right;
        SetNewTimer();
    }

    private void Update()
    {
        if (!CanMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Move();
        ChangeDirectionOnScreenBounds();

        if (Time.time > timer + changeTime)
        {
            ChangeDirection();
            SetNewTimer();
            timer = Time.time;
        }
    }

    private void Move()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    private void ChangeDirection()
    {
        goingRight = !goingRight;
        moveDirection = goingRight ? Vector2.right : Vector2.left;
    }

    private void SetNewTimer()
    {
        changeTime = Random.Range(2f, 5f);
    }

    private void ChangeDirectionOnScreenBounds()
    {
        Vector2 extents = GetComponent<SpriteRenderer>().bounds.extents;


        if (goingRight && transform.position.x + extents.x >= bounds.y) ChangeDirection();
        else if (!goingRight && transform.position.x - extents.x <= bounds.x) ChangeDirection();
    }
}


