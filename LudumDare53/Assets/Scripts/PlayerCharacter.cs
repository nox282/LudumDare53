using UnityEngine;

public class PlayerCharacter : Character
{
    static public PlayerCharacter Get { get; private set; }

    private void Awake()
    {
        Get = this;
    }
}