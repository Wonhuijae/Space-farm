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
#if UNITY_EDITOR
        if (!EventSystem.current.IsPointerOverGameObject())
#else
            if (!EventSystem.current.IsPointerOverGameObject() &&
                EventSystem.current.currentSelectedGameObject == null)
#endif
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
