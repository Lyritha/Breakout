using UnityEngine;

public class ShakeableWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Screenshake.Instance != null) Screenshake.Instance.Shake();
    }
}
