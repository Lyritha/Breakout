using UnityEngine;

public class BossMovementController : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private BossController controller;

    private void Update()
    {
        enemyMovement.CanMove = controller.currentState == BossState.Moving;
    }
}
