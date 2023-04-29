using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComponent : MonoBehaviour
{
    public Action<Collider> OnTriggerEnterEvent;
    public Action<Collider> OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }
}
