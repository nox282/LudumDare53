using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] public float MoveSpeed = 5f;

    public void Move(Vector3 direction)
    {
        direction.y = 0f; // Remove any y component

        Vector3 movement = direction.normalized * MoveSpeed * Time.deltaTime;
        transform.position += movement;
    }
}