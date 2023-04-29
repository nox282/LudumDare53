using UnityEngine;

public class BoxDisguise : MonoBehaviour
{
	[SerializeField] private LayerMask movingLayer;
	[SerializeField] private LayerMask idleLayer;

	[SerializeField] private MovementComponent moveComponent;

	private bool isMoving = true;

	private void Start()
	{
	}

	private void Update()
	{
		var moveAmount = moveComponent.LastMove.magnitude;
		if (moveAmount > 0.01f && !isMoving) // If the object is moving
		{
			isMoving = true;
			gameObject.layer = (int)Mathf.Log(movingLayer.value, 2); // Change the physics layer to the moving layer
		}
		else if (moveAmount < 0.01f && isMoving) // If the object is not moving
		{
			isMoving = false;
			gameObject.layer = (int)Mathf.Log(idleLayer.value, 2); // Change the physics layer to the idle layer
		}
	}
}