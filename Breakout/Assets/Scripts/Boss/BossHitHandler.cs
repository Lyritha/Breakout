using UnityEngine;

public class BossHitHandler : MonoBehaviour
{
    [SerializeField] private LaserBeam2D laser;

    private void Awake()
    {
        laser.onHit += HandleHit;
    }

    void HandleHit(RaycastHit2D hit)
    {
        if (hit.collider.TryGetComponent( out PaddleController _))
        {
            GameEvents.onTakeDamage?.Invoke();
        }
    }
}
