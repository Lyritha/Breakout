using UnityEngine;

public class BossMovement : MonoBehaviour
{

  public Vector4 bounds;
  private float speed = 2f;

  private float time;
  private float changeTime;

  private Rigidbody2D rb;
  private bool goingRight = true;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    bounds = ScreenWorldBounds.GetBounds(Camera.main);

    SetNewTimer();

    rb.linearVelocity = Vector2.right * speed;
  }

  private void Update()
  {
    ChangeDirectionOnScreenBounds();
    if (Time.time > time + changeTime)
    {
      ChangeDirection();
      SetNewTimer();
      time = Time.time;
    }
  }

  private void SetNewTimer()
  {
    changeTime = Random.Range(2f, 5f);
  }

  private void ChangeDirectionOnScreenBounds()
  {
   
    Collider2D bossCollider = GetComponent<Collider2D>();
    Vector2 bossSize = bossCollider.bounds.size;
    Vector2 bossExtents = bossCollider.bounds.extents;
    
    if (goingRight && transform.position.x + bossExtents.x >= bounds.y)
    {
      ChangeDirection();
    }
    else if (!goingRight && transform.position.x - bossExtents.x <= bounds.x)
    {
      ChangeDirection();
    }
    
  }

  private void ChangeDirection()
  {
    goingRight = !goingRight;
    if (goingRight)
    {
      rb.linearVelocity = Vector2.right * speed;
    }
    else
    {
      rb.linearVelocity = Vector2.left * speed;
    }
  }
}

