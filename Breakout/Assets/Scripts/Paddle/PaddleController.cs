using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float keyboardMoveSpeed = 10f;
    [SerializeField] private float mouseFollowSpeed = 8f;
    [SerializeField] private bool useKeyboard = true;
    [SerializeField] private bool useMouse = true;
    [SerializeField] private float mouseMoveThreshold = 0.01f;

    private Camera mainCam;
    private float halfPaddleWidth;
    private float targetX;

    private void Start()
    {
        mainCam = Camera.main;
        targetX = transform.position.x;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            halfPaddleWidth = sr.bounds.extents.x;
        }
        else
        {
            Debug.LogWarning("Paddle heeft geen SpriteRenderer.");
        }
    }

    private void Update()
    {
        float currentX = transform.position.x;
        targetX = currentX;

        // Keyboard input
        if (useKeyboard && Keyboard.current != null)
        {
            float horizontalInput = 0f;

            if (Keyboard.current.aKey.isPressed)
                horizontalInput = -1f;

            if (Keyboard.current.dKey.isPressed)
                horizontalInput = 1f;

            targetX += horizontalInput * keyboardMoveSpeed * Time.deltaTime;
        }

        // Mouse input
        if (useMouse && Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            if (Mathf.Abs(mouseDelta.x) > mouseMoveThreshold)
            {
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

                Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(
                    new Vector3(
                        mouseScreenPos.x,
                        mouseScreenPos.y,
                        Mathf.Abs(mainCam.transform.position.z)
                    )
                );

                targetX = Mathf.Lerp(currentX, mouseWorldPos.x, mouseFollowSpeed * Time.deltaTime);
            }
        }

        Vector4 bounds = ScreenWorldBounds.GetBounds(mainCam);

        float minX = bounds.x + halfPaddleWidth;
        float maxX = bounds.y - halfPaddleWidth;

        targetX = Mathf.Clamp(targetX, minX, maxX);

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}