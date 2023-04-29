using UnityEngine;

public class PlayerCharacter : Character
{
    static public PlayerCharacter Get { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Get = this;
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
}