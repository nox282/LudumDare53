using UnityEngine;

[CreateAssetMenu(fileName = "AlertBehavior", menuName = "Custom/AlertBehavior")]
public class AlertBehavior : ScriptableBehavior
{
    [SerializeField] public float MoveSpeedAdvantage = 2f;

    private MovementComponent movementComponent;

    public override void OnEnter(GameObject Owner)
    {
        base.OnEnter(Owner);

        float moveSpeed = PlayerCharacter.Get.MovementComponent.MoveSpeed + MoveSpeedAdvantage;

        movementComponent = Owner.GetComponent<MovementComponent>();
        if (movementComponent != null)
        {
            movementComponent.MoveSpeed = moveSpeed;
        }
    }

    public override void OnExit(GameObject Owner)
    {
        base.OnExit(Owner);
    }

    public override void OnUpdate(float deltaTime, GameObject Owner)
    {
        base.OnUpdate(deltaTime, Owner);

        Vector3 playerPosition = PlayerCharacter.Get.transform.position;
        Vector3 direction = (playerPosition - Owner.transform.position).normalized;

        movementComponent.Move(direction);
    }
}