using UnityEngine;

public class PlayerCharacter : Character
{
    static public PlayerCharacter Get { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Get = this;
    }
}