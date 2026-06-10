using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health = 3;

    [SerializeField]
    private Image[] healthImage = new Image[3];

    private void OnEnable()
    {
        GameEvents.onBallLost += TakeDamage;
    }
    
    private void OnDisable()
    {
        GameEvents.onBallLost -= TakeDamage;
    }

    public void TakeDamage()
    {
       
        health--;
        UpdateHealth();
        if (health <= 0) GameEvents.onGameOver?.Invoke();

        GameEvents.onBallReset?.Invoke();
    }

    private void UpdateHealth()
    {
        //Revert the health sprites to red if health is greater than 0, otherwise set them to black
        
        for (int i = 0; i < healthImage.Length; i++)
        {
            if (i < health) healthImage[i].color = Color.red;
            else healthImage[i].color = Color.black;
        }
    }
  
}
