using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    public GameObject showCase;
    public GameObject content;

    UIManager UIinstance;

    public event Action<GameObject, GameObject, GameObject[], GameObject[]> OnChangeCategory;

    private void Awake()
    {
        UIinstance = FindObjectOfType<UIManager>();
        OnChangeCategory += UIinstance.SetHighLight;
    }

    public void Open()
    {
        showCase.SetActive(true);
        GetComponent<Outline>().enabled = true;
        OnChangeCategory?.Invoke(gameObject, content, UIinstance.categoryBTNs, UIinstance.contentsShop);
    }
}
