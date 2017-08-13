using UnityEngine;
using Mario;

[RequireComponent(typeof(LineRenderer))]
public class ArcGenerator : MonoBehaviour
{
    public Transform destination;
    public int divisionsPerUnit = 8;

    public float bezierDistance = 0.1f;

    public float displacementStrength = 0.04f;
    public float randomness = 0.3f;
    public float speed = 0.1f;

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Calculate some stuff about the start and end positions.
        Vector3 desToSrc = transform.position - destination.position;
        float distance = desToSrc.magnitude;

        float strength = (bezierDistance * Mathf.Clamp01(distance + 0.00001f));
        // Smaller displacement for smaller distance.

        // For the start point, use the forward direction to determine it.
        Vector3 startControl = transform.position + strength * transform.forward;

        // For the end control point, use the direction to the src to determine it.
        //Vector3 endControl = destination.position + strength * desToSrc.normalized;
        // End control point is not used.

        // Middle control point results in much rounder and smoother curve.
        Vector3 middleControl = (transform.position + destination.position) / 2.0f;
        middleControl += strength * Vector3.ProjectOnPlane(transform.forward, desToSrc.normalized);
        
        // Some debugging.
        Debug.DrawLine(transform.position, startControl, Color.red);
        Debug.DrawLine(destination.position, middleControl, Color.cyan);
        //Debug.DrawLine(destination.position, endControl, Color.blue);

        // Calculate the number of divisions needed.
        int numDivisions = (int)(divisionsPerUnit * distance) + 1;
        lineRenderer.positionCount = numDivisions;

        // Set the start and end point positions.
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(numDivisions - 1, destination.position);

        // Small optimization, calculate out here only once for all the points.
        // Note: Speed is negated so that positive speed values goes toward dest.
        float noiseX = -speed * Time.time;

        // Set the positions along the Bezier curve.
        float tInc = 1f / numDivisions;
        for (int i = 1; i < numDivisions - 1; ++i)
        {
            // Generate offset values for electricity effect.
            float xOff = Mathf.PerlinNoise(noiseX, 0.0f);
            float yOff = Mathf.PerlinNoise(noiseX, 0.1f);
            float zOff = Mathf.PerlinNoise(noiseX, 0.2f);
            // Shift noiseX sampling point by randomness amount.
            noiseX += randomness;

            // Re-map offset values (0, 1) => (-1, 1).
            xOff = ((xOff * 2.0f) - 1.0f) * displacementStrength;
            yOff = ((yOff * 2.0f) - 1.0f) * displacementStrength;
            zOff = ((zOff * 2.0f) - 1.0f) * displacementStrength;

            // Calculate the point on the Bezier curve.
            Vector3 pnt = MathTools.CalculateBezier(tInc * i, transform.position, startControl, middleControl, destination.position);

            // Apply the offsets.
            pnt.x += xOff;
            pnt.y += yOff;
            pnt.z += zOff;

            // Set the position for the line renderer.
            lineRenderer.SetPosition(i, pnt);
        }
    }
}
