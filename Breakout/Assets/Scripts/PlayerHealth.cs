using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health = 3;
    private List<Image> healthImage;


    private void Start()
    {
        
        healthImage = new List<Image>();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            
            if (child.TryGetComponent<Image>(out var image))
            {
                healthImage.Add(image);
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
        if (health <= 0) GameEvents.onGameOver?.Invoke();
    }

    private void UpdateHealth()
    {
        //Revert the health sprites to red if health is greater than 0, otherwise set them to black
        
        for (int i = 0; i < healthImage.Count; i++)
        {
            if (i < health) healthImage[i].color = Color.red;
            else healthImage[i].color = Color.black;
        }
    }
  
}
