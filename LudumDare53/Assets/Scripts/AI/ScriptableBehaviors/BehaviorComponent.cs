using UnityEngine;

public class BehaviorComponent : MonoBehaviour
{
    [SerializeField] private ScriptableBehavior StartBehaviorAsset;

    ScriptableBehavior currentBehavior = null;

    private void Start()
    {
        SetBehavior(StartBehaviorAsset);
    }

    private void Update()
    {
        if (currentBehavior != null)
        {
            currentBehavior.OnUpdate(Time.deltaTime, gameObject);
        }
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
