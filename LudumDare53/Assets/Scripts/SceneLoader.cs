using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
#if UNITY_EDITOR
	[SerializeField] private UnityEditor.SceneAsset sceneAsset;
#endif

	[SerializeField] [HideInInspector]
	private string SceneName;

#if UNITY_EDITOR
	private void OnValidate()
	{
		if(sceneAsset == null)
		{
			SceneName = "";
		}
		else
		{
			SceneName = UnityEditor.AssetDatabase.GetAssetPath(sceneAsset);
		}
	}
#endif
	public void LoadScene()
	{
		SceneManager.LoadScene(sceneAsset.name);
	}
}