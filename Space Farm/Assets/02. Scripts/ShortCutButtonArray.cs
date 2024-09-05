using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShortCutButtonArray : MonoBehaviour
{
    [SerializeField]
    public ItemData[] items_tool;

    public GameObject buttonPrefab;

    private void OnEnable()
    {
        foreach (Transform item in GetComponentInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
        
        foreach(ItemData item in items_tool)
        {
            GameObject tmpBtn = Instantiate(buttonPrefab, transform.position, Quaternion.identity);
            tmpBtn.transform.parent = gameObject.transform;
            tmpBtn.transform.localScale = Vector3.one;

            // tmpBtn.GetComponent<Button>().onClick.AddListener(tmpBtn.GetComponent<ShortCutManager>().OnClickShortCut);
            Image[] tmpImage = tmpBtn.GetComponentsInChildren<Image>();

            foreach (Image img in tmpImage)
            {
                if (img.gameObject.name == "Image") img.sprite = item.Icon_Item;
            }

            tmpBtn.GetComponentInChildren<TextMeshProUGUI>().text = item.shortCutName;
        }
    }
}
