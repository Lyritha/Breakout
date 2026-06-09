using System.Collections;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    [SerializeField] private LaserBeam2D laser;
    [SerializeField] private BossController controller;

    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;
    private void Start()
    {
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        while (controller.bossHealth.GetCurrentHealth() > 0)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            
            controller.currentState = BossState.Attacking;

            laser.StartAttack();

            yield return new WaitForSeconds(1f); // cooldown
            
            controller.currentState = BossState.Moving;
        }
    }

}
