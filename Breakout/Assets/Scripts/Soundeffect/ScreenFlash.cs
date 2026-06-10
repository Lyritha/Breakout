using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField] private Image flashOverlay;
    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private Color flashColor = new Color(1f, 0f, 0f, 0.4f);
    [SerializeField] private AudioClip loseLifeSound;
    [SerializeField] private AudioClip gameOverSound;

    private void OnEnable()
    {
        GameEvents.onBallLost += TriggerFlash;
        GameEvents.onGameOver += TriggerGameOver;
    }

    private void OnDisable()
    {
        GameEvents.onBallLost -= TriggerFlash;
        GameEvents.onGameOver -= TriggerGameOver;
    }

    private void TriggerFlash()
    {
        if (loseLifeSound != null)
            AudioSource.PlayClipAtPoint(loseLifeSound, Camera.main.transform.position);

        StopAllCoroutines();
        StartCoroutine(Flash());
    }

    private void TriggerGameOver()
    {
        if (gameOverSound != null)
            AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);

        StopAllCoroutines();
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        flashOverlay.color = flashColor;
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(flashColor.a, 0f, elapsed / flashDuration);
            flashOverlay.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }

        flashOverlay.color = Color.clear;
    }
}