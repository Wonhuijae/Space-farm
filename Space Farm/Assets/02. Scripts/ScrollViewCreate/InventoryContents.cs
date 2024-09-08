using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContents : MonoBehaviour
{
    GameManager gmInstace;
    UIManager UIinstace;

    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private GameObject[] contents;
    [SerializeField]
    private GameObject[] categoryBTNs;

    private SeedData[] seedData;
    private ToolData[] toolData;
    private CropsData[] cropData;

    private void Awake()
    {
        gmInstace = GameManager.Instance;
        UIinstace = UIManager.instance;
    }
    private void OnEnable()
    {
        UIinstace.SetHighLight(categoryBTNs[0], contents[0], categoryBTNs, contents);
        seedData = gmInstace.GetSeedData();
        toolData = gmInstace.GetToolData();

        SeedContentsSetting();    }

    public void SeedContentsSetting()
    {
        UIinstace.SetHighLight(categoryBTNs[0], contents[0], categoryBTNs, contents);
       RemovingChildren(0);

        foreach(SeedData item in seedData)
        {
            GameObject tmpSlot = Instantiate(slotPrefab, transform.position, Quaternion.identity);
            tmpSlot.transform.parent = contents[0].transform;

            foreach(Image i in tmpSlot.GetComponentsInChildren<Image>())
            {
                if (i.gameObject == tmpSlot) continue;
                i.sprite = item.Icon_Inventory;
            }

            foreach (TextMeshProUGUI t in tmpSlot.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (t.name == "Text_Name") t.text = item.Name;
                else t.text = item.Quantity.ToString();
            }
        }
    }

    public void ToolContentsSetting()
    {
        UIinstace.SetHighLight(categoryBTNs[1], contents[1], categoryBTNs, contents);
        RemovingChildren(1);

        foreach (ToolData item in toolData)
        {
            GameObject tmpSlot = Instantiate(slotPrefab, transform.position, Quaternion.identity);
            tmpSlot.transform.parent = contents[1].transform;

            foreach (Image i in tmpSlot.GetComponentsInChildren<Image>())
            {
                if (i.gameObject == tmpSlot) continue;
                i.sprite = item.Icon_ShortCut;
            }

            foreach (TextMeshProUGUI t in tmpSlot.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (t.name == "Text_Name") t.text = item.Name;
                else t.text = ToStringTier(item.Tier);
            }
        }
    }

    public void CropsContentsSetting()
    {
        //UIinstace.SetHighLight(categoryBTNs[2], contents[2], categoryBTNs, contents);
        //RemovingChildren(2);

        //foreach (var item in gmInstace.GetCropsData())
        //{
        //    GameObject tmpSlot = Instantiate(slotPrefab, transform.position, Quaternion.identity);
        //    //tmpSlot.GetComponent<RectTransform>().SetParent(slotPrefab.GetComponent<RectTransform>(), false);
        //    tmpSlot.transform.parent = contents[2].transform;

        //    foreach (Image i in tmpSlot.GetComponentsInChildren<Image>())
        //    {
        //        if (i.gameObject == tmpSlot) continue;
        //        i.sprite = item.Icon_Inventory;
        //    }

        //    foreach (TextMeshProUGUI t in tmpSlot.GetComponentInChildren<TextMeshProUGUI>())
        //    {
        //        if (t.name == "Text_Name") t.text = item.Name;
        //        else t.text = item.Quantity.ToString();
        //    }
        //}
    }

    public void RemovingChildren(int _idx)
    {
        foreach(Transform o in contents[_idx].GetComponentInChildren<Transform>())
        {
            Debug.Log(o.name);
            //if (o.gameObject == contents[_idx]) continue;
            if (o != null )Destroy(o.gameObject);
        }
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    string ToStringTier(Tier _t)
    {
       switch (_t)
        {
            case Tier.Basic:
                return "기본";
            case Tier.Intermediate:
                return "발전된";
            case Tier.Advanced:
                return "훌륭한";
            default:
                return "Value Error";
        }
    }
}
