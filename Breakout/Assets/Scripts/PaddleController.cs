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
    private bool overwriteKeyboard = false;

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

        // --- NEW: Slowdown factor ---
        bool fastKey = Keyboard.current.leftShiftKey.isPressed
                    || Keyboard.current.rightShiftKey.isPressed;

        bool fastMouse = Mouse.current.rightButton.isPressed;

        float speedFactor = (fastKey || fastMouse) ? 1f : 0.5f;
        // -----------------------------

        // Keyboard input
        if (useKeyboard && Keyboard.current != null)
        {
            float horizontalInput = 0f;

            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                horizontalInput = -1f;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                horizontalInput = 1f;

            targetX += horizontalInput * keyboardMoveSpeed * speedFactor * Time.deltaTime;

            if (horizontalInput != 0)
            {
                Cursor.visible = false;
                overwriteKeyboard = false;
            }
        }

        // Mouse input
        if (useMouse && Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            if (!overwriteKeyboard) overwriteKeyboard = Mathf.Abs(mouseDelta.x) > mouseMoveThreshold;

            if (overwriteKeyboard)
            {
                Cursor.visible = true;
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

                Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(
                    new Vector3(
                        mouseScreenPos.x,
                        mouseScreenPos.y,
                        Mathf.Abs(mainCam.transform.position.z)
                    )
                );

                targetX = Mathf.MoveTowards(
                    currentX,
                    mouseWorldPos.x,
                    mouseFollowSpeed * speedFactor * Time.deltaTime
                );
            }
        }

        // Clamp + apply
        Vector4 bounds = ScreenWorldBounds.GetBounds(mainCam);
        if (TryGetComponent(out SpriteRenderer sr)) halfPaddleWidth = sr.bounds.extents.x;

        float minX = bounds.x + halfPaddleWidth;
        float maxX = bounds.y - halfPaddleWidth;

        targetX = Mathf.Clamp(targetX, minX, maxX);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }

}