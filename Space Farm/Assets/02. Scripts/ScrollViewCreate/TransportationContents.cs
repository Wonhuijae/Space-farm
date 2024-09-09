using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class TransportationContents : MonoBehaviour
{
    public GameObject scrollContent;
    public GameObject ButtonPrefab;
    public TextMeshProUGUI salesPrice;
    public Image salesImage;

    GameManager gmIstance;
    UIManager uiInstance;
    CropsData[] data;

    private void Awake()
    {
        gmIstance = GameManager.Instance;
        uiInstance = UIManager.instance;
        data = gmIstance.GetCropsData();
    }

    private void OnEnable()
    {
        foreach (Transform t in scrollContent.GetComponentInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }

        ContentsSetting();
    }

    void ContentsSetting()
    {
        foreach(var item in data)
        {
            GameObject tmp = Instantiate(ButtonPrefab, scrollContent.transform.position, Quaternion.identity);
            tmp.transform.parent = scrollContent.gameObject.transform;
            tmp.transform.localScale = Vector3.one;

            tmp.GetComponent<Button>().onClick.AddListener(() => SetStand(item));

            foreach(var i in tmp.GetComponentsInChildren<Image>())
            {
                if (i.name == "Image_Thumbnail") i.sprite = item.Icon_Inventory;
            }
            
            foreach(var t in tmp.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (t.name == "Text_Name") t.text = item.Name;
                else t.text = item.Quantity.ToString("N0");
            }
        }
    }

    void SetStand(CropsData _data)
    {
        salesImage.sprite = _data.Icon_Inventory;
        salesPrice.text = _data.SalePrice + " G";
    }

    public void CloseBtn()
    {
        gameObject.SetActive(false);
        uiInstance.OpenPlayPanel();
    }
}
