using UnityEngine;

public class InputComponent : MonoBehaviour
{
    public MovementComponent MovementComponent;

    private void Awake()
    {
        MovementComponent = GetComponent<MovementComponent>();
    }

    private void Update()
    {
        // Get raw input values for horizontal and vertical axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Normalize input values to get direction vector
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Send direction vector to MovementComponent for movement
        MovementComponent.Move(direction);
    }
}