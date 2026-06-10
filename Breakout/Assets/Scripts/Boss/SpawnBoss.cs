using System;
using UnityEngine;
using UnityEngine.UI;


public class SpawnBoss : MonoBehaviour
{
    public static SpawnBoss instance;
    
    [SerializeField] GameObject bossPrefab; 
    [SerializeField] GameObject bossHealth;
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    
    [ContextMenu("spawn")]
    public void Spawn()
    {
        Vector3 spawnPosition = new Vector3(0, 2.38f, 0); //Is ff hard coded weet ik

        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        GameObject ui = Instantiate(bossHealth, canvas.transform);
        
        
        Image healthBar = ui.transform.Find("BossHealthFill").GetComponent<Image>();
        boss.GetComponent<BossHealth>().SetHealthBarFill(healthBar);
        


    }
    
}
