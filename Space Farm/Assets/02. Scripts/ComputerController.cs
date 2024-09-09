using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerController : MonoBehaviour
{
    UIManager UIistance;

    public event Action onCloseComputer;
    public event Action onOpenComputer;

    private void OnEnable()
    {
        UIistance = UIManager.instance;
        if (UIistance != null)
        {
            onCloseComputer += UIistance.CloseComputer;
            onOpenComputer += UIistance.OpenComputer;
        }
    }

    void OnMouseDown()
    {
        if (onOpenComputer != null && !EventSystem.current.IsPointerOverGameObject()) onOpenComputer?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(onCloseComputer != null) onCloseComputer?.Invoke();
        }
    }

    public void ClosePanel()
    {
        onCloseComputer?.Invoke();
    }
}
