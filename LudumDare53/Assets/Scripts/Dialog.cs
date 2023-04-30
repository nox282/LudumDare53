using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
	public Image LeftImage;
	public Image RightImage;

	public DialogData CurrentDialog;
	public TMPro.TextMeshProUGUI Textfield;
	public AudioSource AudioSource;

	private IEnumerator coroutine;

	private void Start()
	{
		if(CurrentDialog != null)
		{
			DisplayDialog(CurrentDialog);
		}
	}

	public void DisplayDialog(DialogData data)
	{
		CurrentDialog = data;

		if(coroutine != null)
		{
			StopCoroutine(coroutine);
		}

		coroutine = DisplayPhases(data);
		StartCoroutine(coroutine);
	}

	private IEnumerator DisplayPhases(DialogData data)
	{
		gameObject.SetActive(true);

		LeftImage.color = data.GetSpeaker(DialogData.SpeakerId.Left).Color;
		RightImage.color = data.GetSpeaker(DialogData.SpeakerId.Right).Color;

		foreach (var phase in data.DialogPhases)
		{
			yield return DisplayPhase(data, phase);
		}
		gameObject.SetActive(false);
	}

	private IEnumerator DisplayPhase(DialogData data, DialogData.DialogPhase phase)
	{
		LeftImage.sprite = phase.OverrideDisplayLeft != null ? phase.OverrideDisplayLeft : data.GetSpeaker(DialogData.SpeakerId.Left).Display;
		RightImage.sprite = phase.OverrideDisplayRight != null ? phase.OverrideDisplayRight : data.GetSpeaker(DialogData.SpeakerId.Right).Display;
		Textfield.text = "";
		Textfield.color = data.GetSpeaker(phase.SpeakerId).Color;

		AudioSource.clip = data.GetSpeaker(phase.SpeakerId).SpeakingSound;
		AudioSource.Play();
		yield return TypeText(phase.Text, phase.LetterDelay);
		AudioSource.Stop();

		while(!Input.anyKeyDown)
		{
			yield return null;
		}
	}

	private IEnumerator TypeText(string fullText, float delay)
	{
		foreach (char c in fullText)
		{
			string displayedText = Textfield.text;
			displayedText += c;
			Textfield.text = displayedText;
			yield return new WaitForSeconds(delay);
		}
	}
}
