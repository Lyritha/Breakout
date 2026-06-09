 using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserBeam2D : MonoBehaviour
{
    public LineRenderer laserBeam;
    public Transform firePoint;
    public float maxDistance = 20f;
    public LayerMask hitMaskLaser;
    public LayerMask hitMaskWarningLaser;

    public LineRenderer warningLine;
    private float warningDuration = 1f;

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
        laserBeam.enabled = false;
        warningLine.enabled = false;
        StartCoroutine(LaserLoop());
    }
    
    void DrawWarningLaser()
    {
        Vector2 origin = firePoint.position;
        Vector2 direction = -firePoint.up;  

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, hitMaskWarningLaser);

        warningLine.SetPosition(0, origin);

        if (hit.collider != null)
            warningLine.SetPosition(1, hit.point);
        else
            warningLine.SetPosition(1, origin + direction * maxDistance);
    }

    IEnumerator LaserLoop()
    {
        while (controller.bossHealth.GetCurrentHealth() > 0)
        {
            
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);

            controller.currentState = BossState.Attacking;
            
            // WARNING PHASE
            DrawWarningLaser();
            warningLine.enabled = true;
            yield return new WaitForSeconds(warningDuration);
            warningLine.enabled = false;
            // 🔥 START LASER
            hasHitPlayer = false;
            laserBeam.enabled = true;

            float t = 0f;

            while (t < fireDuration)
            {
                t += Time.deltaTime;
                ShootLaser();
                
                float progress = t / fireDuration;
                float pulse = Mathf.Pow(Mathf.Sin(progress * Mathf.PI), 2);

                float width = baseWidth + pulse * maxLaserWidth;

                laserBeam.startWidth = width;
                laserBeam.endWidth = width;
                yield return null;
            }

            // 🧊 STOP LASER
            laserBeam.enabled = false;

            controller.currentState = BossState.Moving;
            yield return new WaitForSeconds(1f); // cooldown
        }
    }

    void ShootLaser()
    {
        Vector2 origin = firePoint.position;
        Vector2 direction = -firePoint.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, hitMaskLaser);

        laserBeam.SetPosition(0, origin);

        if (hit.collider != null)
        {
            laserBeam.SetPosition(1, hit.point);

            if (!hasHitPlayer && hit.transform.TryGetComponent<PaddleController>(out var player))
            {
                if (hit.collider.GetComponent<PlayerHealth>() == null) return;
                hasHitPlayer = true;
                hit.collider.GetComponent<PlayerHealth>().TakeDamage();
            }
        }
        else
        {
            laserBeam.SetPosition(1, origin + direction * maxDistance);
        }
    }
}
