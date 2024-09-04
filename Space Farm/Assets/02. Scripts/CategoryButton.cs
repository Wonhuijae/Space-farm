using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    public GameObject showCase;

    UIManager UIinstance;


    public event Action<GameObject> OnChangeCategory;

    private void Awake()
    {
        UIinstance = FindObjectOfType<UIManager>();
        OnChangeCategory += UIinstance.SetHighLight;
    }

    public void Open()
    {
        showCase.SetActive(true);
        GetComponent<Outline>().enabled = true;
        OnChangeCategory?.Invoke(gameObject);
    }
}
