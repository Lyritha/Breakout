using System;
using UnityEngine;
using UnityEngine.UI;


public class SpawnBoss : MonoBehaviour
{    
    [SerializeField] BossHealth bossPrefab; 
    [SerializeField] GameObject bossHealth;
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        bool hasManager = LevelManager.Instance != null;
        if (hasManager)
        {
            LevelManager.LevelChanged += CheckSpawn;
            CheckSpawn(LevelManager.Instance.CurrentLevel);
        }
    }

    private void OnDestroy()
    {
        if (LevelManager.Instance != null) LevelManager.LevelChanged -= CheckSpawn;
    }

    private void CheckSpawn(LevelDefinition definition)
    {
        if (definition.levelType == LevelType.Boss) Spawn();
    }

    [ContextMenu("spawn")]
    public void Spawn()
    {
        Vector3 spawnPosition = new(0, 2.38f, 0);

        BossHealth boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        GameObject ui = Instantiate(bossHealth, canvas.transform);
        ui.transform.SetSiblingIndex(0);


        // dennis this makes me suicidal, but i won't change it for now
        Image healthBar = ui.transform.Find("BossHealthFill").GetComponent<Image>();
        boss.SetHealthBarFill(healthBar);
    }
    
}
