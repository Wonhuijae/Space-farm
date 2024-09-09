using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HologramBTN : MonoBehaviour
{
    public TextMeshProUGUI textQ;

    private TransportationContents tContets;
    public event Action onQuantityChange;

    public int qNum
    {
        get
        {
            return _qNum;
        }
        private set
        {
            _qNum = value;
        }
    }
    private int _qNum;

    private int maxQ;
    public Button PlusBtn;
    public Button MinusBtn;

    private void Awake()
    {
        tContets = GetComponentInParent<TransportationContents>();
        onQuantityChange += tContets.SetSumPrice;

        qNum = 0;
        textQ.text = 0.ToString();

        InteractFalse();
    }

    public void OnPlus()
    {
        qNum++;
        if(qNum > maxQ)
        {
            qNum--;
            PlusBtn.interactable = false;
        }
        if(MinusBtn.interactable == false) MinusBtn.interactable = true;
        textQ.text = qNum.ToString();
        onQuantityChange?.Invoke();
    }

    public void OnMinus()
    {
        qNum--;
        if (qNum <= 0)
        {
            qNum = 0;
            MinusBtn.interactable = false;
        }
        if (PlusBtn.interactable == false) PlusBtn.interactable = true;
        textQ.text = qNum.ToString();
        onQuantityChange?.Invoke();
    }

    public void Reset()
    {
        qNum = 0;
        textQ.text = qNum.ToString();
    }

    public void SetMax(int _m)
    {
        maxQ = _m;
    }

    public void InteractFalse()
    {
        PlusBtn.interactable = false;
        MinusBtn.interactable = false;
    }
    
    public void InteractTrue()
    {
        PlusBtn.interactable = true;
        MinusBtn.interactable = true;
    }
}