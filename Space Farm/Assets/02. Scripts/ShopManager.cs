using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private GoodsData[] goodsData;
    [SerializeField]
    private GameObject prefabPanel;
    [SerializeField]
    private GameObject detailPanel;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < goodsData.Length; i++)
        {
            GeneratrItem(goodsData[i], i);
        }
    }

    void GeneratrItem(GoodsData _item, int _idx)
    {
        GameObject tmpPanel = Instantiate(prefabPanel, gameObject.transform.position, Quaternion.identity);
        tmpPanel.transform.parent = gameObject.transform; // 자식으로 추가
        tmpPanel.transform.localScale = Vector3.one;

        foreach (var t in tmpPanel.GetComponentsInChildren<Image>())
        {
            if (t.gameObject.name == "Image_Thumbnail") t.GetComponent<Image>().sprite = _item.Image;
        }

        foreach(var t in tmpPanel.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if(t.gameObject.name == "Text_Name") t.text = _item.Name;
            else t.text = _item.Price + " G";
        }

        tmpPanel.GetComponent<Button>().onClick.AddListener(() => ShowDetails(_idx));
    }

    void ShowDetails(int _idx)
    {
        detailPanel.SetActive(true);
        foreach (var t in detailPanel.GetComponentsInChildren<Image>())
        {
            if (t.gameObject.name == "Image_Thumbnail") t.GetComponent<Image>().sprite = goodsData[_idx].Image;
        }

        foreach (var t in detailPanel.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (t.gameObject.name == "Text_Name") t.GetComponent<TextMeshProUGUI>().text = goodsData[_idx].Name;
            else if(t.gameObject.name == "Text_Contents") t.GetComponent<TextMeshProUGUI>().text = goodsData[_idx].Description;
            else if(t.gameObject.name == "Text_Price") t.text = goodsData[_idx].Price + " G";
        }
    }
}
