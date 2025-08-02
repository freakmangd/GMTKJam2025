using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public string useMessage = "Use";

    public void Interact()
    {
        onInteract.Invoke();
    }
}
