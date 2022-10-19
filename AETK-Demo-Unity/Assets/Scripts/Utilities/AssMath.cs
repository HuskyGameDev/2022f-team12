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
            // First, Translate vec to be around 0, 0, 0 (relative to b1's center). //
            vec -= b1.bounds.center;

            // Apply our transformations. //
            var b1WorldScale = Vector3.Scale(b1.size, b1.transform.lossyScale);
            var b2WorldScale = Vector3.Scale(b2.size, b2.transform.lossyScale);

            // First, undo b1's rotation on vec.
            var undoRotM = Matrix4x4.Rotate( Quaternion.Inverse(b1.transform.rotation) );
            // Second, scale vec appropriately.
            var scaleM   = Matrix4x4.Scale( Vector3_Divide(b2WorldScale, b1WorldScale) );
            // Third, rotate vec by b2's rotation.
            var rotM     = Matrix4x4.Rotate(b2.transform.rotation);

            // Apply each of the previous transformations, in order.
            vec = (rotM * scaleM * undoRotM).MultiplyPoint(vec);

            // Finally, translate vec to be relative to b2's center.
            vec += b2.bounds.center;

            return vec;
        }
    }
}
