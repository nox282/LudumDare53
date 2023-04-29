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
