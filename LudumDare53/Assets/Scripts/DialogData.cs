using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogData", menuName = "ScriptableObjects/Dialog Data")]
public class DialogData : ScriptableObject
{
	public Texture2D LeftImage;
	public Texture2D RightImage;
	public string Text;
}