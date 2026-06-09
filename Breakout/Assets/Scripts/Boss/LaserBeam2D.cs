using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserBeam2D : MonoBehaviour
{
    public LineRenderer line;
    public Transform firePoint;
    public float maxDistance = 20f;
    public LayerMask hitMask;

    public float minWait = 1f;
    public float maxWait = 5f;
    public float fireDuration = 0.5f;

    private bool hasHitPlayer;

    [SerializeField] BossController controller;
    
    float pulseSpeed = 10f;
    float baseWidth = 0.05f;
    float maxLaserWidth = 0.5f;

    private void Awake()
    {
        controller = GetComponentInParent<BossController>();
    }

    private void Start()
    {
        line.enabled = false;
        StartCoroutine(LaserLoop());
    }

    IEnumerator LaserLoop()
    {
        while (controller.bossHealth.GetCurrentHealth() > 0)
        {
            
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);

            controller.currentState = BossState.Attacking;
            // 🔥 START LASER
            hasHitPlayer = false;
            line.enabled = true;

            float t = 0f;

            while (t < fireDuration)
            {
                t += Time.deltaTime;
                ShootLaser();
                
                float progress = t / fireDuration;
                float pulse = Mathf.Pow(Mathf.Sin(progress * Mathf.PI), 2);

                float width = baseWidth + pulse * maxLaserWidth;

                line.startWidth = width;
                line.endWidth = width;
                yield return null;
            }

            // 🧊 STOP LASER
            line.enabled = false;

            controller.currentState = BossState.Moving;
            yield return new WaitForSeconds(1f); // cooldown
        }
    }

    void ShootLaser()
    {
        Vector2 origin = firePoint.position;
        Vector2 direction = -firePoint.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, hitMask);

        line.SetPosition(0, origin);

        if (hit.collider != null)
        {
            line.SetPosition(1, hit.point);

            if (!hasHitPlayer && hit.transform.TryGetComponent<PaddleController>(out var player))
            {
                hasHitPlayer = true;
                hit.collider.GetComponent<PlayerHealth>().TakeDamage();
            }
        }
        else
        {
            line.SetPosition(1, origin + direction * maxDistance);
        }
    }
}
