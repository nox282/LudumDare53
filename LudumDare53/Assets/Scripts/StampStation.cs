using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampStation : MonoBehaviour
{
	private Animator _animator;
	private AudioSource _audio;
	public AudioClip StampSound;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_audio = GetComponent<AudioSource>();
	}

	private void OnTriggerStay(Collider other)
    {
        PlayerCharacter Player = other.GetComponent<PlayerCharacter>();
        if (Player != null)
        {
			Player.SetIsStamped(true);
			_animator.SetTrigger("StampStampStamp");
		}
    }

	public void PlayAudio()
	{
		_audio.Play();
	}
}
