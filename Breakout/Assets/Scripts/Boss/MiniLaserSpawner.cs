using System;
using UnityEngine;

public class MiniLaserSpawner : MonoBehaviour
{
    private BossController bossController;

    [SerializeField] private GameObject miniLaserPrefab;

    private bool spawned90 = false;
    private bool spawned60 = false;
    private bool spawned30 = false;

    private void Awake()
    {
        bossController = GetComponent<BossController>();
    }

    private void Update()
    {
        float current = bossController.bossHealth.GetCurrentHealth();
        float max = bossController.bossHealth.GetMaxHealth();

        if (!spawned90 && current <= max * 0.9f)
        {
            SpawnMiniLasers();
            spawned90 = true;
        }

        if (!spawned60 && current <= max * 0.6f)
        {
            SpawnMiniLasers();
            spawned60 = true;
        }
        
        if (!spawned30 && current <= max * 0.3f)
        {
            SpawnMiniLasers();
            spawned30 = true;
        }
        
        
    }

    private void SpawnMiniLasers()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0f, -1f, 0f);
        Instantiate(miniLaserPrefab, spawnPosition, Quaternion.identity);
    }
}


   

