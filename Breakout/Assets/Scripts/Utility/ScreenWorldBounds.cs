using UnityEngine;

public static class ScreenWorldBounds
{
    public static Vector4 GetBounds(Camera cam)
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3 camPos = cam.transform.position;

        return new Vector4(
            camPos.x - halfWidth,
            camPos.x + halfWidth,
            camPos.y - halfHeight,
            camPos.y + halfHeight
        );
    }
}