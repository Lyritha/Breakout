using UnityEngine;

public class Block : MonoBehaviour
{
    private SpawnBlocks parent;
    private int score;

    public void Init(SpawnBlocks parent, int score)
    {
        this.parent = parent;
        this.score = score;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (parent != null) parent.SpawnedBlocks.Remove(this);

        Score.Add(score);
        if (Screenshake.Instance != null) Screenshake.Instance.Shake();

        Destroy(gameObject);
    }
}
