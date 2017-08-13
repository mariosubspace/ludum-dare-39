using UnityEngine;
using Mario;

[RequireComponent(typeof(LineRenderer))]
public class BasicBezierLineGenerator : MonoBehaviour
{
    public Transform destination;
    public int divisionsPerUnit = 8;

    public float bezierDistanceFromSource = 0.1f;
    public float bezierDistanceFromDest = 0.1f;

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Calculate some stuff about the start and end positions.
        Vector3 posDelta = destination.position - transform.position;
        float distance = posDelta.magnitude;
        Vector3 startControl = transform.position + bezierDistanceFromSource * transform.forward;
        Vector3 endControl = destination.position + bezierDistanceFromDest * destination.forward;

        // Some debugging.
        Debug.DrawLine(transform.position, startControl, Color.red );
        Debug.DrawLine(destination.position, endControl, Color.cyan);

        // Calculate the number of divisions needed.
        int numDivisions = (int)(divisionsPerUnit * distance) + 1;
        lineRenderer.positionCount = numDivisions;

        // Set the start and end point positions.
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(numDivisions - 1, destination.position);

        // Set the positions along the Bezier curve.
        float tInc = 1f / numDivisions;
        for (int i = 1; i < numDivisions - 1; ++i)
        {
            lineRenderer.SetPosition(i, MathTools.CalculateBezier(tInc * i, transform.position, startControl, endControl, destination.position));
        }
    }
}
