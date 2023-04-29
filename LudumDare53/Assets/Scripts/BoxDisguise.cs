using UnityEngine;

public class BoxDisguise : MonoBehaviour
{
    [SerializeField] private LayerMask movingLayer;
    [SerializeField] private LayerMask idleLayer;

    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private Animator animator;

    private bool isMoving = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var moveAmount = rigidbody.velocity.magnitude;
        if (moveAmount > 0.01f && !isMoving) // If the object is moving
        {
            isMoving = true;
            animator.SetBool("IsBox", false); // Change the animation to the moving animation
            gameObject.layer = (int)Mathf.Log(movingLayer.value, 2); // Change the physics layer to the moving layer
        }
        else if (moveAmount < 0.01f && isMoving) // If the object is not moving
        {
            isMoving = false;
            animator.SetBool("IsBox", true); // Change the animation to the idle animation
            gameObject.layer = (int)Mathf.Log(idleLayer.value, 2); // Change the physics layer to the idle layer
        }
    }
}