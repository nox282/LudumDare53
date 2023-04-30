using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampStation : MonoBehaviour
{
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnTriggerStay(Collider other)
    {
        PlayerCharacter Player = other.GetComponent<PlayerCharacter>();
        if (Player != null)
        {
            Player.isStamped = true;
			_animator.SetTrigger("StampStampStamp");
		}
    }
}
