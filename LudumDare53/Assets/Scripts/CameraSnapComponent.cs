using UnityEngine;

public class CameraSnapComponent : MonoBehaviour
{
    public BoxCollider BoxCollider;
    public float ProjectedOffset = 500;
    public float θ = 45f;

    private Camera mainCamera;

    public void Activate()
    {
        float offset = Mathf.Abs(ProjectedOffset / θ);

        Quaternion rotation = Quaternion.AngleAxis(θ, Vector3.right);
        Vector3 rotatedVector = rotation * Vector3.back;


        Debug.DrawLine(transform.position, transform.position + rotatedVector * 500, Color.red, Time.deltaTime);
        mainCamera = Camera.main;
        mainCamera.transform.position = transform.position + rotatedVector * offset;
        mainCamera.orthographicSize = BoxCollider.size.z * 0.5f;
        mainCamera.aspect = BoxCollider.size.x / BoxCollider.size.z;
    }
}
