using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    private Vector4 bounds;
    
    private void Awake()
    {
        bounds = ScreenWorldBounds.GetBounds(Camera.main);
        SpawnHealth();
    }
    
    private void SpawnHealth()
    {
        GameObject health = new GameObject("Health");
        SpriteRenderer spriteRenderer = health.AddComponent<SpriteRenderer>();
        //Spawn the health at top right of the screen with a little offset from the edge
        health.transform.position = new Vector3(bounds.y - 0.5f, bounds.w - 0.5f, 0f);
        health.transform.SetParent(transform);
    }
}
