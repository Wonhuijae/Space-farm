using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private GameManager gmInstace;

    public GameObject panelDetails;
    public TextMeshProUGUI timeText;
    public AudioSource audioSource;
    public AudioClip closeClip;
    public AudioClip computerClip;
    public AudioClip projectorClip;
    public GameObject curActiveShortCut { get; private set; }

    public GameObject[] categoryBTNs;
    public GameObject[] contentsShop;

    public GameObject PlayerPanel;
    public GameObject ComputerPanel;
    public GameObject InventoryPanel;
    public GameObject ShippingPanel;
    private List<GameObject> PanelGroup = new();

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

        GeneralUISetting();
        PlayerPanel.SetActive(true);

        PanelGroup.AddRange(new GameObject[] { PlayerPanel, ComputerPanel, InventoryPanel ,ShippingPanel });
    }

    private void OnEnable()
    {
        SetHighLight(categoryBTNs[0], contentsShop[0], categoryBTNs, contentsShop);
    }

    public void CloseDetails()
    {
        panelDetails.SetActive(false);
        audioSource.PlayOneShot(closeClip);
    }

    public void GeneralUISetting()
    {
        playerLevel.text = "Lv. " + gmInstace.level;
        playerEXP.text = gmInstace.exp.ToString("N0") + " / " + gmInstace.maxExp.ToString("N0");
        expBar.value = gmInstace.exp;
        expBar.maxValue = gmInstace.maxExp;

        foreach (var t in playerCash)
        {
            t.text = gmInstace.money.ToString("N0");
        }
    }

    public void ChangeActiveShortCut(GameObject _ShortCut, ToolState _tState)
    {
        if (gmInstace.toolState != ToolState.None) // 도구 장비 상태가 None이 아니고
        {
            if (curActiveShortCut != _ShortCut) // 이전에 누른 도구 버튼이 아닐 경우
            {
                curActiveShortCut.GetComponent<Outline>().enabled = false; // 이전 도구 버튼의 아웃라인을 끄고
                _ShortCut.GetComponent<Outline>().enabled = true; // 지금 누른 버튼의 아웃라인을 켜고
                gmInstace.ChangeTool(_tState); // 장비 상태를 바꿔줌
            }
            else // 이전에 누른 도구 버튼을 다시 눌렀을 경우
            {
                curActiveShortCut.GetComponent<Outline>().enabled = false;
                gmInstace.ChangeTool(ToolState.None); // 도구 장비 해제(None)
                gmInstace.ChangeSeed(SeedState.None);
            }
        }
        else // 장비 상태가 None인 경우
        {
            gmInstace.ChangeTool(_tState);
            _ShortCut.GetComponent<Outline>().enabled = true;
        }

        curActiveShortCut = _ShortCut; // 어떤 경우든 아웃라인 비교를 위해 저장해줌;
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
        foreach (var o in _CategoryBTNs)
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

        foreach (var c in _Contents)
        {
            if (c == _content)
            {
                c.SetActive(true);
                continue;
            }
            c.SetActive(false);
        }
    }

    public void RemovingChildren(GameObject _contents)
    {
        foreach (Transform o in _contents.GetComponentInChildren<Transform>())
        {
            if (o != null) Destroy(o.gameObject);
        }
    }

    public void CloseComputer()
    {
        OpenPlayPanel();
        ComputerPanel.SetActive(false);
    }

    public void OpenComputer()
    {
        PlayerPanel.SetActive(false);
        if (!ComputerPanel.activeSelf) audioSource.PlayOneShot(computerClip);
        ComputerPanel.SetActive(true);

        CloseOtherPanel(ComputerPanel);
    }

    public void OpenTransporation()
    {
        PlayerPanel.SetActive(false);
        if (!ShippingPanel.activeSelf) audioSource.PlayOneShot(projectorClip);
        ShippingPanel.SetActive(true);

        CloseOtherPanel(ShippingPanel);
    }

    public void CloseTransporation()
    {
        OpenPlayPanel();
        ShippingPanel.SetActive(false);
    }

    public void OpenInventory()
    {
        PlayerPanel.SetActive(false);
        InventoryPanel.SetActive(true);

        CloseOtherPanel(InventoryPanel);
    }

    public void CloseInventroty()
    {
        OpenPlayPanel();
        InventoryPanel.SetActive(false);
    }

    public void CloseOtherPanel(GameObject o)
    {
        foreach(GameObject i in PanelGroup)
        {
            if(i != o) i.SetActive(false);
        }
    }

    public void OpenPlayPanel()
    {
        PlayerPanel.SetActive(true);
    }
}
