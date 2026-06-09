using System;
using UnityEngine;

public class BossController : MonoBehaviour
{
     public BossState currentState = BossState.Moving;
     public BossHealth bossHealth {private set; get;}

     private void Awake()
     {
         bossHealth = GetComponent<BossHealth>();
     }
}

public enum BossState
{
    Moving,
    Attacking,
    Dying
}
