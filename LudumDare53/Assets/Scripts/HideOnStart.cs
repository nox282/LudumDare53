using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    private void Start()
    {
        var meshrenderer = GetComponent<MeshRenderer>();
        if (meshrenderer != null)
        {
            meshrenderer.enabled = false;
        }
    }
}