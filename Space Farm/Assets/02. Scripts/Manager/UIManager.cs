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
    public GameObject[] categoryBTNs;
    public GameObject[] contentsShop;

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

    [SerializeField]
    private TextMeshProUGUI playerLevel;
    [SerializeField]
    private TextMeshProUGUI playerEXP;
    [SerializeField]
    private TextMeshProUGUI[] playerCash;
    [SerializeField]
    private Slider expBar;

    void Awake()
    {
        gmInstace = GameManager.Instance;

        if (instance != this)
        {
            Destroy(gameObject);
        }

        toolState = ToolState.None;
        if (gmInstace != null)
        {
            gmInstace.onPurchasedItemSeed += UpdateUI;
            gmInstace.onPurchasedItemTool += UpdateUI;
            gmInstace.onGetItemCrops += UpdateUI;
        }

        GeneralUISetting();
    }

    private void OnEnable()
    {
        SetHighLight(categoryBTNs[0], contentsShop[0], categoryBTNs, contentsShop);
    }

    public void CloseDetails()
    {
        panelDetails.SetActive(false);
    }

    void GeneralUISetting()
    {
        playerLevel.text = "Lv. " + gmInstace.level;
        playerEXP.text = gmInstace.exp + " / " + gmInstace.maxExp;
        expBar.value = gmInstace.exp;

        foreach (var t in playerCash)
        {
            t.text = gmInstace.money.ToString("N0");
        }
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

    // 활성화할 버튼, 스크롤뷰
    // 버튼 그룹, 스크롤뷰 그룹
    public void SetHighLight(
                            GameObject _text, 
                            GameObject _content,
                            GameObject[] _CategoryBTNs, 
                            GameObject[] _Contents)
    {
        foreach(var o in _CategoryBTNs)
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

        foreach(var c in _Contents)
        {
            if(c == _content)
            {
                c.SetActive(true);
                continue;
            }
            c.SetActive(false);
        }
    }

    public void UpdateUI(SeedData _d)
    {

    }

    public void UpdateUI(ToolData _d)
    {
        _d.Durability = 10;
    }

    public void UpdateUI(CropsData _d)
    {

    }
    
}
