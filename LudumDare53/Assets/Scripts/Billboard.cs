using UnityEngine;

public class Billboard : MonoBehaviour
{
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