using UnityEngine;

public class MinimapObject : MonoBehaviour
{
    [SerializeField] public bool needsRotationUpdate;
    [SerializeField] public bool toFollow;
    [SerializeField] public MinimapObjectType objectType;

    private void Start()
    {
        MinimapController.Instance?.Add(this);
    }

    private void OnDestroy()
    {
        MinimapController.Instance?.Remove(this);
    }
}