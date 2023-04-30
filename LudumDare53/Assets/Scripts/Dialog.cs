using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
	public AudioClip StartAudio;
	public AudioClip StartAudio2;
	public AudioClip EndAudio;
	public float LetterDelay = 0.02f;

	public Image LeftImage;
	public Image LeftImageBG;
	public Image RightImage;
	public Image RightImageBG;

	public DialogData CurrentDialog;
	public TMPro.TextMeshProUGUI Textfield;
	public AudioSource AudioSource;

	public IEnumerator DisplayDialog(DialogData data)
	{
		CurrentDialog = data;

		yield return DisplayPhases(data);
	}

	private IEnumerator DisplayPhases(DialogData data)
	{
		gameObject.SetActive(true);

		Textfield.text = "";

		LeftImage.enabled = false;
		RightImage.enabled = false;
		LeftImageBG.enabled = false;
		RightImageBG.enabled = false;

		if (StartAudio != null)
		{
			AudioSource.clip = StartAudio;
			AudioSource.Play();
			yield return new WaitForSeconds(StartAudio.length);
			AudioSource.Stop();
		}

		LeftImageBG.color = data.GetSpeaker(DialogData.SpeakerId.Left).Color;
		RightImageBG.color = data.GetSpeaker(DialogData.SpeakerId.Right).Color;

		LeftImage.sprite = data.GetSpeaker(DialogData.SpeakerId.Left).Display;
		RightImage.sprite = data.GetSpeaker(DialogData.SpeakerId.Right).Display;

		LeftImageBG.enabled = true;
		RightImageBG.enabled = true;
		LeftImage.enabled = true;
		RightImage.enabled = true;

		if (StartAudio2 != null)
		{
			AudioSource.clip = StartAudio2;
			AudioSource.Play();
			yield return new WaitForSeconds(StartAudio2.length);
			AudioSource.Stop();
		}

		foreach (var phase in data.DialogPhases)
		{
			yield return DisplayPhase(data, phase);
		}

		if(EndAudio != null)
		{
			AudioSource.clip = EndAudio;
			AudioSource.Play();
			yield return new WaitForSeconds(EndAudio.length);
			AudioSource.Stop();
		}
	}

	private IEnumerator DisplayPhase(DialogData data, DialogData.DialogPhase phase)
	{
		Textfield.text = "";
		Textfield.color = data.GetSpeaker(phase.SpeakerId).Color;

		AudioSource.clip = data.GetSpeaker(phase.SpeakerId).SpeakingSound;
		AudioSource.Play();
		yield return TypeText(phase.Text, LetterDelay);
		AudioSource.Stop();

		while(!Input.anyKey)
		{
			yield return null;
		}

		while (Input.anyKey)
		{
			yield return null;
		}
	}

	private string _displayedText = "";

	private IEnumerator TypeText(string fullText, float delay)
	{
		_displayedText = "";
		foreach (char c in fullText)
		{
			_displayedText += c;
			Textfield.text = _displayedText;
			yield return new WaitForSeconds(delay);

			if(Input.anyKey)
			{
				Textfield.text = fullText;
				while(Input.anyKey)
				{
					yield return null;
				}
				break;
			}
		}
	}
}
