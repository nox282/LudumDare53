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

		public Sprite OverrideDisplayLeft;
		public Sprite OverrideDisplayRight;

		[TextArea(10, 100)]
		public string Text;

		public float LetterDelay = 0.1f;
	}

	public List<DialogPhase> DialogPhases;

	[SerializeField] private Speaker _leftSpeaker;
	[SerializeField] private Speaker _rightSpeaker;

	public Speaker GetSpeaker(SpeakerId id)
	{
		return id == SpeakerId.Left ? _leftSpeaker : _rightSpeaker;
	}
}