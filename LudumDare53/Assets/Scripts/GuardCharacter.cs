using UnityEngine;

public class GuardCharacter : Character
{
    public SpriteRenderer SpriteRenderer;

    public PerceptionComponent PerceptionComponent
    {
        get => GetComponent<PerceptionComponent>();
    }

    public BehaviorComponent BehaviorComponent
    {
        get => GetComponent<BehaviorComponent>();
    }

    public PathComponent PathComponent
    {
        get => GetComponent<PathComponent>();
    }

    protected override void Awake()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnRespawn()
    {
        base.OnRespawn();
        PerceptionComponent.OnRespawn();
        PathComponent.ResetPathOrder();
    }

    public override void OnBeforeActivate()
    {
        base.OnBeforeActivate();
        PathComponent.ResetPathOrder();
        BehaviorComponent.SetIdle();
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

        SpriteRenderer.enabled = isAIEnabled;
    }
}