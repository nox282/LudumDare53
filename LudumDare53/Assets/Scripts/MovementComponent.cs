using UnityEngine;

public class MovementComponent : MonoBehaviour
{
	public Animator Animator;
    public Rigidbody Rigidbody;
    public float MoveSpeed = 5f;

    public Vector3 LastMove { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        direction.y = 0f; // Remove any y component

        LastMove = direction.normalized * MoveSpeed * Time.deltaTime;
        Rigidbody.MovePosition(transform.position + LastMove);

		Animator.SetFloat("speedX", Rigidbody.velocity.x);
		Animator.SetFloat("speedZ", Rigidbody.velocity.z);
	}
}