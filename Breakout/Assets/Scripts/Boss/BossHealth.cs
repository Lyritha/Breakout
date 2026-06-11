using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private float currentHealth = 30;
    private float maxHealth = 30;
    [SerializeField] private Image healthBarFill;
    public Image HealthBarFill => healthBarFill;
    
    //Make the boss color change to red by impact
    private SpriteRenderer bossSpriteRenderer;
    private Coroutine flashCoroutine;
    public event Action OnBossDied;

   



    private void UpdateHealthBar()
    {
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;
    }
    
    //Make the boss color change to red by impact method
    private void FlashRed()
    {
        if (bossSpriteRenderer == null)
        {
            bossSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (bossSpriteRenderer == null) return;

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        StartCoroutine(FlashRedCoroutine());
    }

    private IEnumerator FlashRedCoroutine()
    {
        Color originalColor = bossSpriteRenderer.color;
        bossSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        bossSpriteRenderer.color = originalColor;
    }

    private void TakeDamage(float damage)
    {
        if (Screenshake.Instance != null) Screenshake.Instance.Shake();

        currentHealth -= damage;
        FlashRed();
        UpdateHealthBar();
        Die();
    }
    
    private void Die()
    {
        if (currentHealth <= 0)
        {
            OnBossDied?.Invoke();
            ScoreManager.Instance.AddScore(5000);
            GameEvents.onGameWon?.Invoke();
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<BallBounce>(out var ball))
        {
            float damage = other.transform.position.y > transform.position.y ? 0.5f : 1;
            TakeDamage(damage);
        }
    }
    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealthBarFill(Image healthBarImage)
    {
        healthBarFill = healthBarImage;
        UpdateHealthBar();
    }
    
}