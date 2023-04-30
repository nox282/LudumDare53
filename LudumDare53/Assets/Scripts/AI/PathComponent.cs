using UnityEngine;

public class PathComponent : MonoBehaviour
{
    [SerializeField] private SplineComponent spline;
    [SerializeField] private bool loopPath = true;
    private int currentSplineIndex = 0;
    private bool isGoingForward = true;

    public Vector3 GetDestination()
    {
        Vector3 result = Vector3.zero;
        if (spline)
        {
            result = spline.GetPointPosition(currentSplineIndex);
        }
        return result;
    }

    public void UpdateToNextPoint()
    {
        if (spline)
        {
            currentSplineIndex = spline.GetNextIndex(currentSplineIndex, loopPath, ref isGoingForward);
        }
    }

    public Vector3 FindAndSetClosestPoint(Vector3 Location)
    {
        currentSplineIndex = spline.FindClosestPointIndex(Location);
        return currentSplineIndex >= 0 ? spline.GetPointPosition(currentSplineIndex) : Vector3.zero;
    }

    public void ResetPathOrder()
    {
        currentSplineIndex = 0;
    }
}
