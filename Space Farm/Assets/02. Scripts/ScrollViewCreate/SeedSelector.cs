using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelector : MonoBehaviour
{
    private GameManager gmInstance;
    private UIManager uiInatance;
    private FarmSystem farmSystem;

    private SeedData[] seeds;
    public GameObject seedSelecteBTN;
    public GameObject seedPopup;

    private void Awake()
    {
        gmInstance = GameManager.Instance;
        uiInatance = UIManager.instance;
        seeds = gmInstance.GetSeedData();
        farmSystem = FarmSystem.instance;

        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }
        
        GenerateItem();
    }

    void GenerateItem()
    {
        foreach (SeedData s in seeds)
        {
            GameObject t = Instantiate(seedSelecteBTN, gameObject.transform.position, Quaternion.identity);
            t.transform.parent = gameObject.transform;
            t.transform.localScale = Vector3.one;

            t.GetComponent<Button>().onClick.AddListener(
                () => {
                    farmSystem.SetSeed(s.seedState);
                    seedPopup.SetActive(false);
                      });

            t.GetComponentInChildren<Image>().sprite = s.Icon_Inventory;
            t.GetComponentInChildren<TextMeshProUGUI>().text = s.Name;
        }
    }
}