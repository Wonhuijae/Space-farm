using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortCutManager : MonoBehaviour
{
    public event Action<GameObject, ToolState> OnPressedShortCut;

    private UIManager UIinstance;
    
    public ToolState toolState;

    private void Awake()
    {
        UIinstance = FindObjectOfType<UIManager>();
        if(UIinstance != null )
        {
            OnPressedShortCut += UIinstance.ChangeActiveShortCut;
            GetComponent<Button>().onClick.AddListener( () => UIinstance.ChangeActiveShortCut(gameObject, toolState));
        }
    }
}