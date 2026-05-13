using System;
using UnityEngine;

public class OnBallBottom : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BallBounce>(out BallBounce ballBounce))
        {
            GameEvents.onBallLost?.Invoke();
        }
    }
}
