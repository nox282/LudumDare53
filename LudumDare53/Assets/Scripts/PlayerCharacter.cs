using System;
using UnityEngine;

public class PlayerCharacter : Character
{
    static public PlayerCharacter Get { get; private set; }

    public InputComponent InputComponent;
    private bool isStamped = false;

    public Action<bool> stampStateUpdated;

    protected override void Awake()
    {
        base.Awake();
        Get = this;

        InputComponent = GetComponent<InputComponent>();
    }

    private void OnCollisionStay(Collision other)
    {
        var guardCharacter = other.gameObject.GetComponent<GuardCharacter>();
        if (guardCharacter != null)
        {
            if (CurrentScreenComponent == null)
            {
                throw new System.Exception("shit's wrong bruv");
            }

            CurrentScreenComponent.Respawn();
        }
    }

    public void SetIsStamped(bool value)
    {
        isStamped = value;
        stampStateUpdated(value);
    }

    public bool IsStamped() 
    {
        return isStamped;
    }
}