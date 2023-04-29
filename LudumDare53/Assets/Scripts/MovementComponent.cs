using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] public float MoveSpeed = 5f;

	public Vector3 LastMove { get; set; }

    public void Move(Vector3 direction)
    {
        direction.y = 0f; // Remove any y component

		LastMove = direction.normalized * MoveSpeed * Time.deltaTime;
        transform.position += LastMove;
    }
}