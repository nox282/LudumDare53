using UnityEngine;
using UnityEngine.AI;

public class GuardCharacter : Character
{
    public NavMeshAgent NavMeshAgent;
    public SpriteRenderer SpriteRenderer;

    public PerceptionComponent PerceptionComponent;
    public BehaviorComponent BehaviorComponent;
    public PathComponent PathComponent;

    protected override void Awake()
    {
        base.Awake();

        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        PerceptionComponent = GetComponent<PerceptionComponent>();
        BehaviorComponent = GetComponent<BehaviorComponent>();
        PathComponent = GetComponent<PathComponent>();
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

        if (NavMeshAgent != null)
        {
            NavMeshAgent.enabled = isAIEnabled;
        }

        SpriteRenderer.enabled = isAIEnabled;
    }
}