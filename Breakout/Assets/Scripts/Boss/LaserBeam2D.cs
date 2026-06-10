 using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserBeam2D : MonoBehaviour
{
      [Header("Setup")]
    [SerializeField] private LineRenderer laserBeam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask hitMaskLaser;

    [Header("Warning")]
    [SerializeField] private LineRenderer warningLine;
    [SerializeField] private float warningDuration = 0.5f;
    [SerializeField] private LayerMask warningLayerMask;

    [Header("Timing")]
    [SerializeField] private float fireDuration = 0.5f;

    [Header("Laser")]
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float baseWidth = 0.05f;
    [SerializeField] private float maxLaserWidth = 0.5f;

    public Action<RaycastHit2D> onHit;

    private bool hasHit;

    private bool isFiring;

    private void Start()
    {
        laserBeam.enabled = false;
        warningLine.enabled = false;
    }

    public void StartAttack()
    {
        if (!isFiring)
            StartCoroutine(LaserRoutine());
    }

    private IEnumerator LaserRoutine()
    {
        isFiring = true;

        warningLine.enabled = true;

        float i = 0f;

        while (i < warningDuration)
        {
            i += Time.deltaTime;

            DrawWarning();

            yield return null;
        }

        warningLine.enabled = false;

        hasHit = false;
        laserBeam.enabled = true;

        float t = 0f;

        while (t < fireDuration)
        {
            t += Time.deltaTime;
            ShootLaser();

            float p = t / fireDuration;
            float pulse = Mathf.Pow(Mathf.Sin(p * Mathf.PI), 2);

            float w = baseWidth + pulse * maxLaserWidth;
            laserBeam.startWidth = w;
            laserBeam.endWidth = w;

            yield return null;
        }

        laserBeam.enabled = false;
        isFiring = false;
    }

    private void DrawWarning()
    {
        Vector2 origin = firePoint.position;
        Vector2 dir = -firePoint.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, maxDistance, warningLayerMask);

        warningLine.SetPosition(0, origin);
        warningLine.SetPosition(1,
            hit.collider ? hit.point : origin + dir * maxDistance);
        warningLine.startWidth = maxLaserWidth;
        
    }

    private void ShootLaser()
    {
        Vector2 origin = firePoint.position;
        Vector2 dir = -firePoint.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, maxDistance, hitMaskLaser);

        laserBeam.SetPosition(0, origin);

        if (hit.collider == null)
        {
            laserBeam.SetPosition(1, origin + dir * maxDistance);
            return;
        }

        laserBeam.SetPosition(1, hit.point);

        if (hasHit) return;

        hasHit = true;
        onHit?.Invoke(hit);
    }

    public float GetFireDuration()
    {
        return fireDuration;
    }
}
