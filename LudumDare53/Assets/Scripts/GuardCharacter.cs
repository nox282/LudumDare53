using UnityEngine;

public class GuardCharacter : Character
{
    public PerceptionComponent PerceptionComponent
    {
        get => GetComponent<PerceptionComponent>();
    }

    public BehaviorComponent BehaviorComponent
    {
        get => GetComponent<BehaviorComponent>();
    }
}