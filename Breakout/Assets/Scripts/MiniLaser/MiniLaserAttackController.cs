using System.Collections;
using UnityEngine;

public class MiniLaserAttackController : MonoBehaviour
{
    [SerializeField] private LaserBeam2D laser;
    [SerializeField] private MiniLaserController controller;

    [SerializeField] private BossHealth bossHealth;
    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;

    private bool isBossDead = false;
    private void Start()
    {
        StartCoroutine(Loop());
    }

    
    private void Awake()
    {
        bossHealth = FindAnyObjectByType<BossHealth>();
        bossHealth.OnBossDied += HandleBossDeath;
    }

    private void OnDestroy()
    {
        bossHealth.OnBossDied -= HandleBossDeath;
    }
    
    private void HandleBossDeath()
    {
        isBossDead = true;
    }
    IEnumerator Loop()
    {
        while (!isBossDead)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            
            controller.currentState = MiniLaserState.Attacking;

            laser.StartAttack();

            yield return new WaitForSeconds(1f); // cooldown
            
            controller.currentState = MiniLaserState.Moving;
        }
    }
}
