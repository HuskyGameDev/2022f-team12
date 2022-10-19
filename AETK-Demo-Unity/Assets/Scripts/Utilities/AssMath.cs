using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamRotten
{
    public static class AssMath
    {

        /// <summary>
        /// Performs a component-wise division between two Vector3s.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Vector3 Vector3_Divide(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3
            (
                lhs.x / rhs.x,
                lhs.y / rhs.y,
                lhs.z / rhs.z
            );
        }

        // Transform a point between two Collision boxes.
        public static Vector3 BoxTransform(Vector3 vec, BoxCollider b1, BoxCollider b2)
        {
            // Create a translation matrix between the two Colliders. //
            var trs = Matrix4x4.TRS
            (
                b2.bounds.center - b1.bounds.center,
                b2.transform.rotation * Quaternion.Inverse(b1.transform.rotation),
                Vector3_Divide(Vector3.Scale(b2.size, b2.transform.lossyScale), Vector3.Scale(b1.size, b1.transform.lossyScale))
            );

            // Apply the TRS matrix to vec. //
            return trs.MultiplyPoint(vec);
        }
    }
}
