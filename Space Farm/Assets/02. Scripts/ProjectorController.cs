using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectorController : MonoBehaviour
{
    UIManager UIinstance;
    PlayerManager playerInstance;

    private void Awake()
    {
        UIinstance = UIManager.instance;
        playerInstance = PlayerManager.instance;
    }

    private void OnMouseDown()
    {
#if UNITY_EDITOR
        if (!EventSystem.current.IsPointerOverGameObject())
#else
        if(!playerInstance.IsPointerOverUIObject())
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
