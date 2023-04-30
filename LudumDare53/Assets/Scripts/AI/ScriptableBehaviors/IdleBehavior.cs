using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "IdleBehavior", menuName = "Custom/IdleBehavior")]
public class IdleBehavior : ScriptableBehavior
{
    [SerializeField] public float IdleMoveSpeed = 1f;
    [SerializeField] public float WanderRadius = 50f;
    [SerializeField] public float TimeBeforeNewDirectionIsPickedInSeconds = 5f;

    NavMeshAgent NavMeshAgent = null;
    Vector3 currentDestination;
    float nextDirectionPickTimestamp = float.MinValue;

    public override void OnEnter(GameObject Owner)
    {
        base.OnEnter(Owner);

        NavMeshAgent = Owner.GetComponent<NavMeshAgent>();

        if (NavMeshAgent != null)
        {
            NavMeshAgent.speed = IdleMoveSpeed;
        }
    }

    public override void OnUpdate(float deltaTime, GameObject Owner)
    {
        base.OnUpdate(deltaTime, Owner);

        if (Time.time > nextDirectionPickTimestamp)
        {
            currentDestination = GetRandomPositionAroundOwner(Owner);
            nextDirectionPickTimestamp = Time.time + TimeBeforeNewDirectionIsPickedInSeconds;
        }

        if (NavMeshAgent != null)
        {
            NavMeshAgent.destination = currentDestination;
        }
    }

    public override void OnExit(GameObject Owner)
    {
        base.OnExit(Owner);
    }

    private Vector3 GetRandomPositionAroundOwner(GameObject Owner)
    {
        return Owner.transform.position + Random.onUnitSphere * WanderRadius;
    }
}