using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public AudioSource FootstepAudio;
    public Animator Animator;
    public ScreenComponent CurrentScreenComponent;

    virtual protected void Awake()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    virtual protected void Start()
    {
        RefreshScreenComponent();
    }

    virtual protected void Update()
    {
        RefreshScreenComponent();

        // Update animation properties
        Animator.SetFloat("speedX", Rigidbody.velocity.x);
        Animator.SetFloat("speedZ", Rigidbody.velocity.z);
        Animator.SetFloat("speed", Rigidbody.velocity.sqrMagnitude);
    }

    public virtual void OnRespawn()
    {

    }

    public virtual void OnBeforeActivate()
    {

    }

    public virtual void OnAfterActivate()
    {

    }

    virtual public void RefreshScreenComponent()
    {
        if (CurrentScreenComponent != null)
        {
            if (CurrentScreenComponent.Box.bounds.Contains(transform.position))
            {
                return;
            }
        }

        var screenComponents = FindObjectsOfType<ScreenComponent>();
        foreach (var screenComponent in screenComponents)
        {
            if (screenComponent.Box.bounds.Contains(transform.position))
            {
                CurrentScreenComponent = screenComponent;
                break;
            }
        }
    }

    public void PlayFootstepEvent()
    {
        FootstepAudio.Play();
    }
}