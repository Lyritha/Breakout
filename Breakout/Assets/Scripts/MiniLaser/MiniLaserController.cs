using UnityEngine;

public class MiniLaserController : MonoBehaviour
{
    public MiniLaserState currentState = MiniLaserState.Moving;
}

public enum MiniLaserState
{
    Moving,
    Attacking,
}

