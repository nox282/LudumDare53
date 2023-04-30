using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Animator Animator;
    public float MoveSpeed = 5f;

    public float Y = .5f;

    private Character owner;

    public Vector3 InputVelocity { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        owner = GetComponent<Character>();
        Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        InputVelocity = Vector3.zero;
    }

    private void OnDisable()
    {
        Rigidbody.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = InputVelocity;

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

        Vector3 position = transform.position;
        position.y = Y;
        transform.position = position;

        Animator.SetFloat("speedX", Rigidbody.velocity.x);
        Animator.SetFloat("speedZ", Rigidbody.velocity.z);
    }

    public void Move(Vector3 direction)
    {
        direction.y = 0f; // Remove any y component
        InputVelocity = direction.normalized * MoveSpeed;
    }

    public bool MoveTo(Vector3 destination)
    {
        float distBuffer = 0.55f;
        float dist = Vector3.Distance(transform.position, destination);
        if (dist < distBuffer)
        {
            return true;
        }
        Vector3 direction = destination - transform.position;
        Move(direction);
        return false;
    }
}