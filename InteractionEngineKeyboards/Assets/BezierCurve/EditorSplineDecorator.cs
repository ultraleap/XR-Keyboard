using System.Collections.Generic;
using UnityEngine;

public class EditorSplineDecorator : MonoBehaviour
{

    public BezierSpline spline;

    private int frequency = 1;

    public bool lookForward;

    
    public List<float> distances = new List<float>();

    [ContextMenu("Align Objects")]
    public void AlignObjects()
    {
        spline = GetComponent<BezierSpline>();
        if (frequency <= 0)
        {
            return;
        }
        float stepSize = frequency * transform.childCount;
        List<Vector3> positionsOnCurve = spline.GetLinearlySpacedPoints(transform.childCount);
        distances.Clear();
        if (spline.Loop || stepSize == 1)
        {
            stepSize = 1f / stepSize;
        }
        else
        {
            stepSize = 1f / (stepSize - 1);
        }
        Debug.Log(spline.Length().ToString("0.00000"));
        for (int p = 0, f = 0; f < frequency; f++)
        {
            for (int i = 0; i < transform.childCount; i++, p++)
            {
                Vector3 position = positionsOnCurve[i];
                transform.GetChild(i).position = positionsOnCurve[i];
                if (lookForward)
                {
                    transform.GetChild(i).LookAt(position + (Quaternion.Euler(0, -90, 0) * spline.GetDirection(p * stepSize)));
                }
            }
        }

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            distances.Add(Vector3.Distance(transform.GetChild(i).position, transform.GetChild(i + 1).position));
        }
    }
}