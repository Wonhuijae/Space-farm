using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortCutManager : MonoBehaviour
{
    public event Action<GameObject> OnPressedShortCut;

    private UIManager UIinstance;
    private GameManager gameInstance;

    private void Awake()
    {
        gameInstance = FindObjectOfType<GameManager>();
        UIinstance = FindObjectOfType<UIManager>();
        if(UIinstance != null )
        {
            OnPressedShortCut += UIinstance.ChangeActiveShortCut;
        }

        GetComponent<Button>().onClick.AddListener(OnClickShortCut);
    }

    public void OnClickShortCut()
    {
        OnPressedShortCut?.Invoke(gameObject);
    }
}
