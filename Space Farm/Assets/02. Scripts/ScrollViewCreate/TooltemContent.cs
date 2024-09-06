using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolItemContent : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPanel;
    [SerializeField]
    private GameObject detailPanel;
    [SerializeField]
    private Button purchaseBTN;

    private ToolData[] itemData;

    private GameManager gmInstance;

    private void Awake()
    {
        gmInstance = FindObjectOfType<GameManager>();
        itemData = gmInstance.GetToolData();
    }

    void OnEnable()
    {
        foreach(Transform o in GetComponentInChildren<Transform>())
        {
            Destroy(o.gameObject);
        }

        for (int i = 0; i < itemData.Length; i++)
        {
            GenerateItem(itemData[i], i);
        }
    }

    void GenerateItem(ToolData _item, int _idx)
    {
        GameObject tmpPanel = Instantiate(prefabPanel, gameObject.transform.position, Quaternion.identity);
        tmpPanel.transform.parent = gameObject.transform; // 자식으로 추가
        tmpPanel.transform.localScale = Vector3.one; // 크기 조정

        foreach (var t in tmpPanel.GetComponentsInChildren<Image>()) // 상점 아이콘
        {
            if (t.gameObject.name == "Image_Thumbnail") t.GetComponent<Image>().sprite = _item.Icon_Shop;
        }

        foreach(var t in tmpPanel.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if(t.gameObject.name == "Text_Name") t.text = _item.Name; // 이름
            else t.text = _item.Price + " G"; // 가격
        }

        tmpPanel.GetComponent<Button>().onClick.AddListener(() => ShowDetails(_idx)); // 해당 아이템 데이터 넘김
    }
    void ShowDetails(int _idx)
    {
        if (gmInstance != null) purchaseBTN.GetComponent<Button>().onClick.AddListener(() => gmInstance.TryToPurchaseTool(itemData[_idx]));
        else Debug.Log("게임매니저 없음");

        detailPanel.SetActive(true);
        foreach (var t in detailPanel.GetComponentsInChildren<Image>())
        {
            if (t.gameObject.name == "Image_Thumbnail") t.GetComponent<Image>().sprite = itemData[_idx].Icon_Shop;
        }

        foreach (var t in detailPanel.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (t.gameObject.name == "Text_Name") t.GetComponent<TextMeshProUGUI>().text = itemData[_idx].Name;
            else if(t.gameObject.name == "Text_Contents") t.GetComponent<TextMeshProUGUI>().text = itemData[_idx].Description;
            else if(t.gameObject.name == "Text_Price") t.text = itemData[_idx].Price + " G";
        }
    }
}
