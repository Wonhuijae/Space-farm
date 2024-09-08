using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShortCutButtonArray : MonoBehaviour
{
    private ToolData[] items_tool;

    public GameObject buttonPrefab;
    private GameManager gmInstance;

    private void OnEnable()
    {
        gmInstance = GameManager.Instance;
        items_tool = gmInstance.GetToolData();

        foreach (Transform item in GetComponentInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
        
        foreach(ToolData item in items_tool)
        {
            GameObject tmpBtn = Instantiate(buttonPrefab, transform.position, Quaternion.identity);
            tmpBtn.transform.parent = gameObject.transform;
            tmpBtn.transform.localScale = Vector3.one;

            tmpBtn.GetComponent<ShortCutManager>().toolState = item.toolState;

            Image[] tmpImage = tmpBtn.GetComponentsInChildren<Image>();

            foreach (Image img in tmpImage)
            {
                if (img.gameObject.name == "Image") img.sprite = item.Icon_ShortCut;
            }

            tmpBtn.GetComponentInChildren<TextMeshProUGUI>().text = item.ShortcutName;
        }
    }
}
