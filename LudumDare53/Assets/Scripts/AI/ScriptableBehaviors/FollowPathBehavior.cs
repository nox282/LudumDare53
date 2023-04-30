using UnityEngine;

[CreateAssetMenu(fileName = "FollowPathBehavior", menuName = "Custom/FollowPathBehavior")]
public class FollowPathBehavior : ScriptableBehavior
{
    [SerializeField] public float MoveSpeed = 1f;

    MovementComponent movementComponent = null;
    PathComponent pathComponent = null;

    Vector3 currentDestination = Vector3.zero;

    public override void OnEnter(GameObject Owner)
    {
        base.OnEnter(Owner);

        movementComponent = Owner.GetComponent<MovementComponent>();
        pathComponent = Owner.GetComponent<PathComponent>();

        if (movementComponent != null)
        {
            movementComponent.MoveSpeed = MoveSpeed;
        }

        currentDestination = GetDestinationAndUpdateNextPoint();
    }

    public override void OnUpdate(float deltaTime, GameObject Owner)
    {
        base.OnUpdate(deltaTime, Owner);

        if (movementComponent != null && pathComponent != null)
        {

            if (movementComponent.IsStuck)
            {
                Owner.transform.position = currentDestination;
                movementComponent.ResetStuckState();
            }

            bool alreadyAtDestination = movementComponent.MoveTo(currentDestination);
            if (alreadyAtDestination)
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
