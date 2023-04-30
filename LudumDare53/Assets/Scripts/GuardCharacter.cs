using UnityEngine;

public class GuardCharacter : Character
{
    public PerceptionComponent PerceptionComponent
    {
        get => GetComponent<PerceptionComponent>();
    }

    public BehaviorComponent BehaviorComponent
    {
        get => GetComponent<BehaviorComponent>();
    }

    public override void OnRespawn()
    {
        base.OnRespawn();
        PerceptionComponent.OnRespawn();
    }

    public void EnableAI(bool isAIEnabled)
    {
        if (PerceptionComponent != null)
        {
            PerceptionComponent.enabled = isAIEnabled;
        }

        if (BehaviorComponent != null)
        {
            BehaviorComponent.enabled = isAIEnabled;
        }

        if (MovementComponent != null)
        {
            MovementComponent.enabled = isAIEnabled;
        }
    }
}