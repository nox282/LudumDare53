using UnityEngine;

public class GuardCharacter : Character
{
    public BehaviorComponent BehaviorComponent;

    private void Awake()
    {
        BehaviorComponent = GetComponent<BehaviorComponent>();
    }
}