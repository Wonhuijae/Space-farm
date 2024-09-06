using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private GameManager gmInstace;

    public GameObject panelDetails;
    public TextMeshProUGUI timeText;
    public GameObject curActiveShortCut { get; private set; }
    public ToolState toolState {  get; private set; }
    public GameObject[] showCase;
    public GameObject[] contents;

    public GameObject PlayerPanel;
    public GameObject ComputerPanel;
    public GameObject InventoryPanel;
    public GameObject ShippingPanel;

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
        gmInstace = FindObjectOfType<GameManager>();

        if (instance != this)
        {
            Destroy(gameObject);
        }

        toolState = ToolState.None;
    }

    private void OnEnable()
    {
        SetHighLight(showCase[0], contents[0]);
    }

    public void CloseDetails()
    {
        panelDetails.SetActive(false);
    }

    public void ChangeActiveShortCut(GameObject _ShortCut, ToolState _tState)
    {
        if (toolState != ToolState.None) // 도구 장비 상태가 None이 아니고
        {
            if (curActiveShortCut != _ShortCut) // 이전에 누른 도구 버튼이 아닐 경우
            {
                curActiveShortCut.GetComponent<Outline>().enabled = false; // 이전 도구 버튼의 아웃라인을 끄고
                _ShortCut.GetComponent<Outline>().enabled = true; // 지금 누른 버튼의 아웃라인을 켜고
                toolState = _tState; // 장비 상태를 바꿔줌
            }
            else // 이전에 누른 도구 버튼을 다시 눌렀을 경우
            {
                curActiveShortCut.GetComponent<Outline>().enabled = false;
                toolState = ToolState.None; // 도구 장비 해제(None)
            }
        }
        else // 장비 상태가 None인 경우
        {
            toolState = _tState;
            _ShortCut.GetComponent<Outline>().enabled = true;
        }

        curActiveShortCut = _ShortCut; // 어떤 경우든 아웃라인 비교를 위해 저장해줌
        gmInstace.ChangeTool(toolState);
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

    public void GetItem(SeedData seed)
    {
        Debug.Log(gmInstace.GetItem(seed));
    }
}
