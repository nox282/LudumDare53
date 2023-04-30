using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogData", menuName = "ScriptableObjects/Dialog Data")]
public class DialogData : ScriptableObject
{
	public enum SpeakerId
	{
		Left,
		Right
	}

	[System.Serializable]
	public class Speaker
	{
		public Sprite Display;
		public AudioClip SpeakingSound;
		public Color Color;
	}

	[System.Serializable]
	public class DialogPhase
	{
		public SpeakerId SpeakerId;

		[TextArea(3, 20)]
		public string Text;
	}

	public List<DialogPhase> DialogPhases;

	[SerializeField] private Speaker _leftSpeaker;
	[SerializeField] private Speaker _rightSpeaker;

	public Speaker GetSpeaker(SpeakerId id)
	{
		return id == SpeakerId.Left ? _leftSpeaker : _rightSpeaker;
	}
}