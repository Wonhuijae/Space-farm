using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private GameManager gameInstace;

    public GameObject panelDetails;
    //public TextMeshProUGUI text;
    public GameObject[] shortCutBTNs;
    public TextMeshProUGUI timeText;
    public GameObject curActiveShortCut { get; private set; }
    public ToolState toolState {  get; private set; }
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
        gameInstace = FindObjectOfType<GameManager>();

        if (instance != this)
        {
            Destroy(gameObject);
        }
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

    public void ChangeActiveShortCut(GameObject _ShortCut)
    {
        if (curActiveShortCut != _ShortCut) 
        {
            curActiveShortCut.GetComponent<Outline>().enabled = false;
        }
        _ShortCut.GetComponent<Outline>().enabled = true;
        curActiveShortCut = _ShortCut;

        gameInstace.ChangeTool(_ShortCut.GetComponent<ITools>().CurToolState());
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
