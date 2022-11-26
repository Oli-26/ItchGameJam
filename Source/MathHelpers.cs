using Godot;
using System;

public static class MathHelpers
{
    public static Vector3 Lerp(Vector3 a, Vector3 b, float weight)
    {
        return new Vector3(
            Mathf.Lerp(a.x, b.x, weight),
            Mathf.Lerp(a.y, b.y, weight),
            Mathf.Lerp(a.z, b.z, weight)
        );
    }
}
