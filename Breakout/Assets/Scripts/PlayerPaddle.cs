using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isScaled = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void HalveScale()
    {
        if (isScaled) return;

        transform.localScale = new Vector3(
            originalScale.x / 2f,
            originalScale.y,
            originalScale.z
        );

        isScaled = true;

        Invoke(nameof(ResetScale), 10f);
    }

    private void ResetScale()
    {
        transform.localScale = originalScale;
        isScaled = false;
    }
}