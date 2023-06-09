using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float MoveSpeed = 5f;
    public float Y = .5f;

    private Character owner;
    public Vector3 InputVelocity { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        owner = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = InputVelocity;

        // Constrain character to screen bounds
        if (owner != null && owner.CurrentScreenComponent != null)
        {
            Bounds bounds = owner.CurrentScreenComponent.Box.bounds;

            if (!bounds.Contains(transform.position))
            {
                Vector3 closestPoint = bounds.ClosestPoint(transform.position);
                Vector3 delta = closestPoint - transform.position;
                Debug.DrawLine(transform.position, closestPoint, Color.red, 5);
                Rigidbody.velocity = delta * 500;
            }
        }

        // Constrain character's Y
        Vector3 position = transform.position;
        position.y = Y;
        transform.position = position;
    }

    public void Move(Vector3 direction)
    {
        direction.y = 0f; // Remove any y component
        InputVelocity = direction.normalized * MoveSpeed;
    }
}