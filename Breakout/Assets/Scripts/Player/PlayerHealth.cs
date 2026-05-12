using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health = 3;
    private List<SpriteRenderer> healthSprites;


    private void Start()
    {
        
        healthSprites = new List<SpriteRenderer>();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);

            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                healthSprites.Add(spriteRenderer);
            }
        }
    }

    private void OnEnable()
    {
        GameEvents.onBallLost += TakeDamage;
    }
    
    private void OnDisable()
    {
        GameEvents.onBallLost -= TakeDamage;
    }

    private void TakeDamage()
    {
       
        health--;
        UpdateHealth();
        if (health <= 0)
        {
            GameEvents.onGameOver?.Invoke();
        }
    }

    private void UpdateHealth()
    {
        //Revert the health sprites to red if health is greater than 0, otherwise set them to black
        
        for (int i = 0; i < healthSprites.Count; i++)
        {
            if (i < health)
            {
                healthSprites[i].color = Color.red;
            }
            else
            {
                healthSprites[i].color = Color.black;
            }
        }
    }
  
}
