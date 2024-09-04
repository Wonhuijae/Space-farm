using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject panelDetails;
    //public TextMeshProUGUI text;
    public GameObject[] shortCutBTNs;
    public TextMeshProUGUI timeText;

    public int curActiveShortCut { get; private set; }

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
        //Debug.Log(_ShortCut);
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
}
