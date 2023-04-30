using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "FollowPathBehavior", menuName = "Custom/FollowPathBehavior")]
public class FollowPathBehavior : ScriptableBehavior
{
    public float MoveSpeed = 1f;
    public float AcceptableRadius = 1f;

    NavMeshAgent NavMeshAgent;
    PathComponent pathComponent = null;

    Vector3 currentDestination = Vector3.zero;

    public override void OnEnter(GameObject Owner)
    {
        base.OnEnter(Owner);

        NavMeshAgent = Owner.GetComponent<NavMeshAgent>();
        pathComponent = Owner.GetComponent<PathComponent>();

        if (NavMeshAgent != null)
        {
            NavMeshAgent.speed = MoveSpeed;
        }

        currentDestination = pathComponent.FindAndSetClosestPoint(Owner.transform.position);
    }

    public override void OnUpdate(float deltaTime, GameObject Owner)
    {
        base.OnUpdate(deltaTime, Owner);

        if (NavMeshAgent != null && pathComponent != null)
        {
            NavMeshAgent.destination = currentDestination;
            if ((Owner.transform.position - currentDestination).sqrMagnitude < AcceptableRadius * AcceptableRadius)
            {
                currentDestination = GetDestinationAndUpdateNextPoint();
            }
        }
    }

    public override void OnExit(GameObject Owner)
    {
        base.OnExit(Owner);
    }

    private Vector3 GetDestinationAndUpdateNextPoint()
    {
        var destination = pathComponent.GetDestination();
        pathComponent.UpdateToNextPoint();
        return destination;
    }
}
