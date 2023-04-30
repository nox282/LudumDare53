using UnityEngine;

public class Character : MonoBehaviour
{
    public ScreenComponent CurrentScreenComponent;
    public MovementComponent MovementComponent;
    public AudioSource FootstepAudio;

    virtual protected void Awake()
    {
        MovementComponent = GetComponent<MovementComponent>();
    }

    virtual protected void Start()
    {
        RefreshScreenComponent();
    }

    virtual protected void Update()
    {
        RefreshScreenComponent();
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