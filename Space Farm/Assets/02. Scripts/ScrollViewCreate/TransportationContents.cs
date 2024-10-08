using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransportationContents : MonoBehaviour
{
    public GameObject scrollContent;
    public GameObject ButtonPrefab;
    public TextMeshProUGUI salesPrice;
    public TextMeshProUGUI salesPriceSum;
    public TextMeshProUGUI playerCash;
    public Image salesImage;
    public HologramBTN hBtn;

    public event Action OnItemSwitch;

    GameManager gmInstance;
    UIManager uiInstance;
    CropsData[] data;
    CropsData select;

    private int sum;
    private int price;

    private void Awake()
    {
        gmInstance = GameManager.Instance;
        uiInstance = UIManager.instance;
        data = gmInstance.GetCropsData();

        OnItemSwitch += hBtn.Reset;

        InitPanel();
    }

    private void OnEnable()
    {
        ContentsSetting();
        InitPanel();
    }

    private void InitPanel()
    {
        sum = 0;
        price = 0;
        salesImage.enabled = false;
        salesPrice.enabled = false;

        salesPriceSum.text = 0.ToString() + " G";

        hBtn.Reset();
    }

    void ContentsSetting()
    {
        foreach (Transform t in scrollContent.GetComponentInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }

        uiInstance.GeneralUISetting();

        foreach (var item in data)
        {
            GameObject tmp = Instantiate(ButtonPrefab, scrollContent.transform.position, Quaternion.identity);
            tmp.transform.parent = scrollContent.gameObject.transform;
            tmp.transform.localScale = Vector3.one;

            tmp.GetComponent<Button>().onClick.AddListener
                (() =>
                {
                    salesImage.enabled = true;
                    salesPrice.enabled = true;
                    SetStand(item);
                    OnItemSwitch?.Invoke();
                    price = item.SalePrice;
                    hBtn.SetMax(gmInstance.GetQuantity(item.Code));
                    hBtn.InteractTrue();
                    select = item;
                });

            foreach(var i in tmp.GetComponentsInChildren<Image>())
            {
                if (i.name == "Image_Thumbnail") i.sprite = item.Icon_Inventory;
            }

            foreach (var t in tmp.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (t.name == "Text_Name") t.text = item.Name;
                else
                {
                    int q = gmInstance.GetQuantity(item.Code);
                    t.text = q.ToString("N0");

                    if (q <= 0) tmp.GetComponent<Button>().interactable = false;
                    else tmp.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    void SetStand(CropsData _data)
    {
        salesImage.sprite = _data.Icon_Inventory;
        salesPrice.text = _data.SalePrice + " G";
        hBtn.InteractFalse();
    }

    public void SetSumPrice()
    {
        sum = hBtn.qNum * price;
        salesPriceSum.text = sum.ToString("N0") + " G";
    }


    public void CloseBtn()
    {
        gameObject.SetActive(false);
        uiInstance.OpenPlayPanel();
    }

    public void Send()
    {
        gmInstance.SendCrops(hBtn.qNum, sum, select);
        ContentsSetting();

        hBtn.InteractFalse();

        InitPanel();
    }
}
