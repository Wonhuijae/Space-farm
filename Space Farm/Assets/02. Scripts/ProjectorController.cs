using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectorController : MonoBehaviour
{
    UIManager UIinstance;

    private void Awake()
    {
        UIinstance = UIManager.instance;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            UIinstance.OpenTransporation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIinstance.CloseTransporation();
        }
    }
}
