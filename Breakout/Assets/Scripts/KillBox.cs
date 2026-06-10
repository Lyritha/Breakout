using System;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BallBounce ballBounce))
        {
            GameEvents.onBallLost?.Invoke();
            GameEvents.onBallReset?.Invoke();
        }
    }
}
