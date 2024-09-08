using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HologramBTN : MonoBehaviour
{
    public TextMeshProUGUI textQ;

    private int qNum = 0;
    public Button PlusBtn;
    public Button MinusBtn;
    public void OnPlus()
    {
        qNum++;
        if(MinusBtn.interactable == false) MinusBtn.interactable = true;
        textQ.text = qNum.ToString();
    }

    public void OnMinus()
    {
        qNum--;
        if (qNum == 0)
        {
            qNum = 0;
            MinusBtn.interactable = false;
        }
        if (PlusBtn.interactable == false) PlusBtn.interactable = true;
        textQ.text = qNum.ToString();
    }
}