using UnityEngine;

[CreateAssetMenu(fileName = "IdleBehavior", menuName = "Custom/IdleBehavior")]
public class IdleBehavior : ScriptableBehavior
{
    [SerializeField] public float IdleMoveSpeed = 1f;
    [SerializeField] public float TimeBeforeNewDirectionIsPickedInSeconds = 5f;

    MovementComponent movementComponent = null;
    Vector3 direction = Vector3.zero;
    float nextDirectionPickTimestamp = float.MinValue;

    public override void OnEnter(GameObject Owner)
    {
        base.OnEnter(Owner);

        movementComponent = Owner.GetComponent<MovementComponent>();

        if (movementComponent != null)
        {
            movementComponent.MoveSpeed = IdleMoveSpeed;
        }
    }

    public override void OnUpdate(float deltaTime, GameObject Owner)
    {
        base.OnUpdate(deltaTime, Owner);

        if (Time.time > nextDirectionPickTimestamp)
        {
            PickRandomDirection();
            nextDirectionPickTimestamp = Time.time + TimeBeforeNewDirectionIsPickedInSeconds;
        }

        if (movementComponent != null)
        {
            movementComponent.Move(direction);
        }
    }

    public override void OnExit(GameObject Owner)
    {
        base.OnExit(Owner);
    }

    private void PickRandomDirection()
    {
        direction = Random.onUnitSphere;
    }
}