using System;
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

    public static Bounds GetBounds2D(Camera cam)
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3 center = cam.transform.position;
        Vector3 size = new Vector3(halfWidth * 2f, halfHeight * 2f, 0f);

        return new Bounds(center, size);
    }

    public static Bounds GetPaddedBounds2D(Camera cam, PaddingStruct padding)
    {
        Bounds b = GetBounds2D(cam);

        // Expand the bounds by padding
        float newWidth = b.size.x + padding.left + padding.right;
        float newHeight = b.size.y + padding.top + padding.bottom;

        // Shift the center because padding is asymmetric
        Vector3 newCenter = b.center;
        newCenter.x += (padding.right - padding.left) * 0.5f;
        newCenter.y += (padding.top - padding.bottom) * 0.5f;

        return new Bounds(newCenter, new Vector3(newWidth, newHeight, 0f));
    }


    [Serializable]
    public struct PaddingStruct
    {
        public float top;
        public float bottom;
        public float left;
        public float right;
    }
}