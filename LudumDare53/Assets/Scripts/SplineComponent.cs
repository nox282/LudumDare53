using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineComponent : MonoBehaviour
{
    [SerializeField] private bool drawDebug = true;

    private Transform[] points;

    // Start is called before the first frame update
    void Start()
    {
        int childCount = transform.childCount;
        points = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    public Vector3 GetPointPosition(int index)
    {
        Vector3 result = Vector3.zero;
        if (IsValidIndex(index))
        {
            result = points[index].position;
        }
        return result;
    }

    public int GetNextIndex(int index, bool isLoop, ref bool isGoingForward)
    {
        int nextIndex = isGoingForward ? index + 1 : index - 1;
        if (!IsValidIndex(nextIndex))
        {
            if (isLoop)
            {
                nextIndex = isGoingForward ? 0 : points.Length - 1;
            }
            else 
            {
                isGoingForward = !isGoingForward;
                nextIndex = isGoingForward ? index + 1 : index - 1;
            }
        }
        return nextIndex;
    }
    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < points.Length;
    }

    private void OnDrawGizmos()
    {
        if (drawDebug)
        {
            int childCount = transform.childCount;
            Transform[] debugPoints = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                debugPoints[i] = transform.GetChild(i);
            }

            Gizmos.color = Color.gray;
            for (int i = 1; i < childCount; i++)
            {
                Gizmos.DrawLine(debugPoints[i-1].position, debugPoints[i].position);
            }
            Gizmos.color = Color.red;
            if (childCount > 1)
            {
                Gizmos.DrawLine(debugPoints[childCount - 1].position, debugPoints[0].position);
            }
        }
    }
}
