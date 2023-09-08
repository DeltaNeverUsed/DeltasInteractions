using UnityEngine;

namespace DeltasInteractions.Extensions
{
    public static class Vec3Extensions
    {
        public static Vector3 Clamp(this Vector3 vector3, float min, float max)
        {
            return Vector3.Normalize(vector3) * Mathf.Clamp(Vector3.Magnitude(vector3), min, max);
        }
    }
}
