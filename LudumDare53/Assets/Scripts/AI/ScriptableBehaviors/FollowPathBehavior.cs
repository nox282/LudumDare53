using UnityEngine;

[CreateAssetMenu(fileName = "FollowPathBehavior", menuName = "Custom/FollowPathBehavior")]
public class FollowPathBehavior : ScriptableBehavior
{
    [SerializeField] public float MoveSpeed = 1f;

    MovementComponent movementComponent = null;
    PathComponent pathComponent = null;

    public override void OnEnter(GameObject Owner)
    {
        base.OnEnter(Owner);

        movementComponent = Owner.GetComponent<MovementComponent>();
        pathComponent = Owner.GetComponent<PathComponent>();

        if (movementComponent != null)
        {
            movementComponent.MoveSpeed = MoveSpeed;
        }
        if (pathComponent)
        {
            Vector3 dest = pathComponent.GetDestination();
            if (dest != Vector3.zero)
            {
                Owner.transform.position = dest;
                pathComponent.UpdateToNextPoint();
            }
        }
    }

    public override void OnUpdate(float deltaTime, GameObject Owner)
    {
        base.OnUpdate(deltaTime, Owner);

        if (movementComponent != null && pathComponent != null)
        {
            bool alreadyAtDestination = movementComponent.MoveTo(pathComponent.GetDestination());
            if (alreadyAtDestination)
            {
                pathComponent.UpdateToNextPoint();
            }
        }
    }

    public override void OnExit(GameObject Owner)
    {
        base.OnExit(Owner);
    }
}
