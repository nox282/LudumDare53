using UnityEngine;

public class Billboard : MonoBehaviour
{
	/*public bool lockXAxis = false;
	public bool lockYAxis = false;

	private void LateUpdate()
	{
		// Get the direction from the camera to this object
		Vector3 directionToCamera = Camera.main.transform.position;// - transform.position;

		// Make the object face the camera
		Quaternion lookRotation = Quaternion.LookRotation(-directionToCamera);

		// Lock the X and/or Y axis if needed
		if (lockXAxis || lockYAxis)
		{
			Vector3 eulerAngles = lookRotation.eulerAngles;
			if (lockXAxis)
			{
				eulerAngles.x = 0;
			}
			if (lockYAxis)
			{
				eulerAngles.y = 0;
			}
			lookRotation = Quaternion.Euler(eulerAngles);
		}

		transform.rotation = lookRotation;
	}*/

	[SerializeField] private bool lockXRotation = false;
	[SerializeField] private bool lockYRotation = false;
	[SerializeField] private bool lockZRotation = false;

	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void LateUpdate()
	{

		var originalX = transform.eulerAngles.x;
		var originalY = transform.eulerAngles.y;
		var originalZ = transform.eulerAngles.z;

		Vector3 cameraPlaneNormal = -mainCamera.transform.forward;

		var target = transform.position - cameraPlaneNormal;
		
		transform.LookAt(target, Vector3.up);

		var rotation = transform.eulerAngles;
		if (lockXRotation) rotation.x = originalX;
		if (lockYRotation) rotation.y = originalY;
		if (lockZRotation) rotation.z = originalZ;
		transform.eulerAngles = rotation;
	}
}