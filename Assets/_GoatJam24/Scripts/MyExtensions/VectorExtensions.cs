using UnityEngine;

namespace _GoatJam24.Scripts.MyExtensions
{
    public static class VectorExtensions
    {
        public static Vector3 With(this Vector3 origin, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? origin.x, y ?? origin.y, z ?? origin.z);
        }
        public static Vector3 WithAdd(this Vector3 origin, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x + origin.x ?? origin.x, y + origin.y ?? origin.y, z + origin.z ?? origin.z);
        }
        public static Vector3 WithRemove(this Vector3 origin, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x + origin.x ?? origin.x, y - origin.y ?? origin.y, z + origin.z ?? origin.z);
        }
    }
}