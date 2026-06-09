using UnityEngine;

public class MiniLaserMovementController : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private MiniLaserController controller;

    private void Update()
    {
        enemyMovement.CanMove = controller.currentState == MiniLaserState.Moving;
    }
}
