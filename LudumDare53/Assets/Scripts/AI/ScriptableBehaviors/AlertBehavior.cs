using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "AlertBehavior", menuName = "Custom/AlertBehavior")]
public class AlertBehavior : ScriptableBehavior
{
    [SerializeField] public float MoveSpeedAdvantage = 2f;

    private MovementComponent movementComponent;
    private NavMeshAgent navMeshAgent;

    public override void OnEnter(GameObject Owner)
    {
        base.OnEnter(Owner);

        float moveSpeed = PlayerCharacter.Get.MovementComponent.MoveSpeed + MoveSpeedAdvantage;
        navMeshAgent = Owner.GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.speed = moveSpeed;
        }
    }

    public override void OnExit(GameObject Owner)
    {
        base.OnExit(Owner);
    }

    public override void OnUpdate(float deltaTime, GameObject Owner)
    {
        base.OnUpdate(deltaTime, Owner);
        navMeshAgent.destination = PlayerCharacter.Get.transform.position;
    }
}