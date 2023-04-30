using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
	public DialogData CurrentDialog;

    public void DisplayDialog(DialogData data)
	{
		CurrentDialog = data;
	}
}
