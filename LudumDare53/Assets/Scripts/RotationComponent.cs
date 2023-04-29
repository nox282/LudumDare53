using UnityEngine;

public class RotationComponent : MonoBehaviour
{
    private Vector3 previousPosition;
    private Vector3 currentVelocity;

    // Debug arrow settings
    [SerializeField] private bool drawDebugArrow = true;
    [SerializeField] private float debugArrowLength = 1f;
    [SerializeField] private float debugArrowHeadLength = 0.25f;
    [SerializeField] private float debugArrowHeadAngle = 20f;
    [SerializeField] private Color debugArrowColor = Color.white;

    private Quaternion lastRotation;

    private void Start()
    {
        // Initialize rotation to be facing the positive Z axis
        lastRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation = lastRotation;
    }

    private void Update()
    {
        // Compute current velocity of object based on its position
        currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
        RotateToVelocity();
    }

    public void RotateToVelocity()
    {
        // Compute the cardinal rotation angle based on the current velocity of the object
        Vector3 direction = currentVelocity.normalized;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Set the rotation to left or right if the angle is within a certain range
            Debug.Log(angle);
            if (Mathf.Abs(angle) > 44f && Mathf.Abs(angle) < 136f)
            {
                angle = angle > 0f ? 90f : -90f;
            }
            else
            {
                angle = Mathf.Round(angle / 90f) * 90f;
            }

            // Rotate object to face cardinal direction
            lastRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = lastRotation;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawDebugArrow)
        {
            // Draw a debug arrow to show the forward direction of the object
            Gizmos.color = debugArrowColor;
            Vector3 endPos = transform.position + transform.forward * debugArrowLength;
            Gizmos.DrawLine(transform.position, endPos);

            // Draw the arrowhead
            Vector3 right = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 180 + debugArrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 180 - debugArrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawLine(endPos, endPos + right * debugArrowHeadLength);
            Gizmos.DrawLine(endPos, endPos + left * debugArrowHeadLength);
            Gizmos.DrawLine(endPos + right * debugArrowHeadLength, endPos + left * debugArrowHeadLength);
        }
    }
}