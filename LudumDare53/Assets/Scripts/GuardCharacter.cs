using UnityEngine;

public class GuardCharacter : Character
{
    public BehaviorComponent BehaviorComponent;

    protected override void Awake()
    {
        base.Awake();
        BehaviorComponent = GetComponent<BehaviorComponent>();
    }
}