using UnityEngine;

public class BehaviorComponent : MonoBehaviour
{
    public ScriptableBehavior IdleBehaviorAsset;

    private ScriptableBehavior currentBehavior = null;

    private void Start()
    {
        SetBehavior(IdleBehaviorAsset);
    }

    private void Update()
    {
        if (currentBehavior != null)
        {
            currentBehavior.OnUpdate(Time.deltaTime, gameObject);
        }
    }

    public void SetIdle()
    {
        SetBehavior(IdleBehaviorAsset);
    }

    public void SetBehavior(ScriptableBehavior behavior)
    {
        if (currentBehavior != null)
        {
            currentBehavior.OnExit(gameObject);
        }

        currentBehavior = Instantiate(behavior);

        if (currentBehavior != null)
        {
            currentBehavior.OnEnter(gameObject);
        }
    }
}
