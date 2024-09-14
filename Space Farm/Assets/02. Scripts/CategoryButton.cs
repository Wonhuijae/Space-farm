using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    public GameObject showCase;
    public GameObject content;

    public AudioSource audioSource;
    public AudioClip openclip;

    UIManager UIinstance;

    public event Action<GameObject, GameObject, GameObject[], GameObject[]> OnChangeCategory;

    private void Awake()
    {
        UIinstance = FindObjectOfType<UIManager>();
        OnChangeCategory += UIinstance.SetHighLight;
    }

    public void Open()
    {
        audioSource.PlayOneShot(openclip);
        showCase.SetActive(true);
        content.SetActive(true);
        GetComponent<Outline>().enabled = true;
        OnChangeCategory?.Invoke(gameObject, showCase, UIinstance.categoryBTNs, UIinstance.contentsShop);
    }
}
