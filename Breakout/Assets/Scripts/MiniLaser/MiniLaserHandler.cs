using UnityEngine;

public class MiniLaserHandler : MonoBehaviour
{
    [SerializeField] private LaserBeam2D laser;

    private void Awake()
    {
        laser.onHit += HandleHit;
    }

    void HandleHit(RaycastHit2D hit)
    {
        if (hit.collider.TryGetComponent<PlayerHealth>(out var player))
        {
            player.TakeDamage();
        }
    }
}
