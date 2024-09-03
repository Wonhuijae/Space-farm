using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject panelDetails;
    public TextMeshProUGUI text;
    public GameObject[] shortCutBTNs;

    private int curActiceShortCut;

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

        curActiceShortCut = -1;
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
        if (curActiceShortCut >= 0 && curActiceShortCut != _ShortCut) 
        {
            shortCutBTNs[curActiceShortCut].GetComponent<Outline>().enabled = false;
        }
        shortCutBTNs[_ShortCut].GetComponent<Outline>().enabled = true;
        curActiceShortCut = _ShortCut;
    }
}
