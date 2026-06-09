 using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserBeam2D : MonoBehaviour
{
    private float maxDistance = 20f;
    private bool hasHitPlayer;
    [SerializeField] private LineRenderer laserBeam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask hitMaskLaser;
    [SerializeField] private LayerMask hitMaskWarningLaser;
    [SerializeField] LineRenderer warningLine;
    
    //Changeable in inspector for balance changes if needed
    [SerializeField] float warningDuration = 0.5f;
    [SerializeField] float minWait = 1f;
    [SerializeField] float maxWait = 5f;
    [SerializeField] float fireDuration = 0.5f;
    [SerializeField] BossController controller;
    [SerializeField]float baseWidth = 0.05f;
    [SerializeField]float maxLaserWidth = 0.5f;

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
            
            //Attack indicator
            DrawWarningLaser();
            warningLine.enabled = true;
            yield return new WaitForSeconds(warningDuration);
            warningLine.enabled = false;
            //Starts firing the laser
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

            //Stop firing
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

        if (hit.collider == null)
        {
            laserBeam.SetPosition(1, origin + direction * maxDistance);
            return;
        }

        laserBeam.SetPosition(1, hit.point);

        if (hasHitPlayer)
            return;

        if (!hit.transform.TryGetComponent<PaddleController>(out _))
            return;

        if (!hit.collider.TryGetComponent<PlayerHealth>(out var health))
            return;

        hasHitPlayer = true;
        health.TakeDamage();
    }
}
