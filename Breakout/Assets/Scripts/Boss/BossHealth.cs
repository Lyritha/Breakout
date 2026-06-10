using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private int currentHealth = 20;
    private int maxHealth = 20;
    [SerializeField] private Image healthBarFill;
    
    //Make the boss color change to red by impact
    private SpriteRenderer bossSpriteRenderer;
    private Coroutine flashCoroutine;
    public event Action OnBossDied;

    private void Start()
    {
        UpdateHealthBar();
    }

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

    private void TakeDamage(int damage)
    {
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
            GameEvents.onGameWon?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<BallBounce>(out var ball))
        {
            TakeDamage(1);
        }
    }
    
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}