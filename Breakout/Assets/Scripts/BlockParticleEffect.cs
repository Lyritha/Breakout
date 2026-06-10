using UnityEngine;

public class BlockParticleEffect : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;

    public void SpawnParticles()
    {
        if (particlePrefab == null) return;

        GameObject effect = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ParticleSystem.MainModule main = ps.main;
                main.startColor = sr.color;
            }
        }

        Destroy(effect, 1f);
    }
}