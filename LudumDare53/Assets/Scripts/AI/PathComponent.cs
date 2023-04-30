using UnityEngine;

public class PathComponent : MonoBehaviour
{
    [SerializeField] private GameObject spline;
    [SerializeField] private bool loopPath = true;
    private int currentSplineIndex = 0;
    private bool isGoingForward = true;

    public Vector3 GetDestination()
    {
        Vector3 result = Vector3.zero;
        if (spline)
        {
            result = spline.GetComponent<SplineComponent>().GetPointPosition(currentSplineIndex);
        }
        return result;
    }

    public void UpdateToNextPoint()
    {
        if (spline)
        {
            currentSplineIndex = spline.GetComponent<SplineComponent>().GetNextIndex(currentSplineIndex, loopPath, ref isGoingForward);
        }
    }

    public void OnRespawn()
    {
        currentSplineIndex = 0;
    }
}
