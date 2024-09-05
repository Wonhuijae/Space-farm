using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject panelDetails;
    //public TextMeshProUGUI text;
    public GameObject[] shortCutBTNs;
    public TextMeshProUGUI timeText;
    public int curActiveShortCut { get; private set; }
    public GameObject[] showCase;
    public GameObject[] contents;

    string normalColorCode = "#FFFFFF84";
    Color normalColor
    {
        get
        {
            if (ColorUtility.TryParseHtmlString(normalColorCode, out _color)) return _color;
            else return Color.white;
        }
    }
    Color _color;
    public static UIManager instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<UIManager>();
            }
            return m_Instance;
        }
    }
    private static UIManager m_Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        curActiveShortCut = -1;
    }

    private void OnEnable()
    {
        SetHighLight(showCase[0], contents[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseDetails()
    {
        panelDetails.SetActive(false);
    }

    public void ChangeActiveShortCut(int _ShortCut)
    {
        if (curActiveShortCut >= 0 && curActiveShortCut != _ShortCut) 
        {
            shortCutBTNs[curActiveShortCut].GetComponent<Outline>().enabled = false;
        }
        shortCutBTNs[_ShortCut].GetComponent<Outline>().enabled = true;
        curActiveShortCut = _ShortCut;
    }

    public void SetTime(int _h, int _m)
    {
        timeText.text = $"{_h} : {_m}";
    }

    public void SetHighLight(GameObject _text, GameObject _content)
    {
        foreach(var o in showCase)
        {
            if (o == _text)
            {
                o.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                o.GetComponent<Outline>().enabled = true;
                
                continue;
            }
            o.GetComponent<Outline>().enabled = false;
            o.GetComponentInChildren<TextMeshProUGUI>().color = normalColor;
        }

        foreach(var c in contents)
        {
            if(c == _content)
            {
                c.SetActive(true);
                continue;
            }
            c.SetActive(false);
        }
    }
}
