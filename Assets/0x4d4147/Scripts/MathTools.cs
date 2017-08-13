using UnityEngine;

namespace Mario
{
    public class MathTools : MonoBehaviour
    {
        /// <summary>
        /// Calculates a point along a Bezier curve defined by the four points and parameterized by t.
        /// </summary>
        /// <param name="t">0 to 1, the parameter for the position along the curve.</param>
        /// <param name="p0">The start point.</param>
        /// <param name="p1">The start point control.</param>
        /// <param name="p2">The end point control.</param>
        /// <param name="p3">The end point.</param>
        /// <returns>The point along the curve.</returns>
        public static Vector3 CalculateBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float tt = t * t;
            float ttt = t * tt;
            float f = 1 - t;
            float ff = f * f;
            float fff = f * ff;

            return (fff * p0) + (3 * ff * t * p1) + (3 * f * tt * p2) + (ttt * p3);
        }
    }
}