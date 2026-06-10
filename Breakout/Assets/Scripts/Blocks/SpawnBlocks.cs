using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    [SerializeField] 
    private LevelDefinition level;

    [SerializeField]
    private ScreenWorldBounds.PaddingStruct padding;
    [SerializeField]
    private float spacing = 0.1f;

    [SerializeField]
    private SpriteRenderer blockPrefab;

    private bool hasGenerated = false;
    private bool canWin = true;

    public List<Block> SpawnedBlocks { get; private set; } = new List<Block>();

    private void Awake()
    {
        GenerateLevel();
    }

    private void Update()
    {
        if (hasGenerated && SpawnedBlocks.Count == 0 && canWin)
        {
            // you won!
            canWin = false;
            GameEvents.onGameWon?.Invoke();
        }
    }

    public void GenerateLevel()
    {
        Bounds bounds = ScreenWorldBounds.GetPaddedBounds2D(Camera.main, padding);

        int rowCount = level.rows.Length;

        // 1. Find widest row
        int widest = 0;
        foreach (LevelRow row in level.rows) widest = Mathf.Max(widest, row.slots.Length);

        // 2. Compute cell size based on widest row
        Vector2 cellSize = new(bounds.size.x / widest, bounds.size.y / rowCount);
        Vector2 blockSize = new(cellSize.x - spacing, cellSize.y - spacing);

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            GenerateRow(rowIndex, bounds, cellSize, blockSize);

        hasGenerated = true;
    }

    private void GenerateRow(int rowIndex, Bounds bounds, Vector2 cellSize, Vector2 blockSize)
    {
        LevelRow row = level.rows[rowIndex];
        int blocksInRow = row.slots.Length;

        // 4. Horizontal centering offset for shorter rows
        float totalRowWidth = blocksInRow * cellSize.x;
        float offsetX = (bounds.size.x - totalRowWidth) * 0.5f;

        // 5. Compute row Y position
        float y = bounds.max.y - (rowIndex + 0.5f) * cellSize.y;
        for (int col = 0; col < blocksInRow; col++)
        {
            if (!row.slots[col]) continue;

            // 6. Compute block center X
            float x = bounds.min.x + offsetX + (col + 0.5f) * cellSize.x;

            // 7. Spawn block
            SpriteRenderer block = Instantiate(blockPrefab, new Vector3(x, y, 0f), Quaternion.identity, transform);

            block.size = new Vector2(blockSize.x, blockSize.y);
            block.color = row.color;

            if (block.TryGetComponent(out Block blockComponent))
            {
                SpawnedBlocks.Add(blockComponent);
                blockComponent.Init(this, row.score);
            }
        }
    }



    private void OnDrawGizmos()
    {
        Bounds b = ScreenWorldBounds.GetPaddedBounds2D(Camera.main, padding);
        Gizmos.DrawWireCube(b.center, b.size);
    }


}

