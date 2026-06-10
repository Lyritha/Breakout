using System;
using UnityEngine;

public static class GameEvents
{
    public static Action onBallLost;
    public static Action onBallReset;
    public static Action onTakeDamage;

    public static Action onGameWon;
    public static Action onGameOver;
}
