using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public static Screenshake Instance;

    [Header("Settings")]
    public float shakeDuration = 0.2f;
    public float shakeStrength = 0.2f;

    private float shakeTimer;
    private Vector3 originalPos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.unscaledDeltaTime;
            transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * shakeStrength;
        }
        else
        {
            transform.localPosition = originalPos;
        }
    }

    public void Shake()
    {
        shakeTimer = shakeDuration;
    }
}
