using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class FieldCycle : MonoBehaviour
{
    enum GrowState
    {
        none,
        seed,
        sprout,
        crops
    }
    GrowState state;

    private GameObject popUp;
    public SeedItem seed;

    private int growDay;
    float time = 0;
    bool isSeed = false;
    bool isSprout = false ;
    bool isCrops = false;

    GameManager gmInstace;
    FarmSystem farmSystem;

    List<GameObject> poses = new();
    List<GameObject> plants = new();
    private int posIdx = 0;
    private Image growImage;
    private Slider growSlider;

    void Awake()
    {
        state = GrowState.none;

        gmInstace = GameManager.Instance;
        farmSystem = FarmSystem.instance;
        popUp = farmSystem.SeedPopup;

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t == transform || t.name == "Seeds" || t.name == "Sprout" || t.name == "Adult" || t.name == "field") continue;
            if (t.GetComponent<RectTransform>() != null)
            {
                if (t.GetComponent<Image>() != null)
                {
                    growImage = t.GetComponent<Image>();
                    growImage.gameObject.SetActive(false);
                }
                else if (t.GetComponent<Slider>() != null)
                {
                    growSlider = t.GetComponent<Slider>();
                    growSlider.gameObject.SetActive(false);
                }
            }
            else poses.Add(t.gameObject);
        }
    }

    private void Update()
    {
        if (state == GrowState.none || seed == null) return;

        time += Time.deltaTime;
        growSlider.value = time;

        if (time > growDay / 3 && state == GrowState.seed && !isSprout) Sprouting();
        if (time > growDay && state == GrowState.sprout && !isCrops) PlantingFruit();
        
    }

    void Sowing()
    {
        state = GrowState.seed;
        isSeed = true;
        for(; posIdx < 2; posIdx++)
        {
            InstatatePrefab(seed.Sowing(), poses[posIdx]);
        }

        growSlider.gameObject.SetActive(true);
        growSlider.value = 0;
        growSlider.maxValue = growDay;

        growImage.gameObject.SetActive(true);
        growImage.sprite = seed.SeedData.Icon_Shop;
    }

    void Sprouting()
    {
        state = GrowState.sprout;
        isSprout = true;
        RemovingPrefab();

        for (; posIdx < 5;posIdx++)
        {
            InstatatePrefab(seed.Sprouting(), poses[posIdx]);
        }
    }

    void PlantingFruit()
    {
        state = GrowState.crops;
        isCrops = true;
        
        RemovingPrefab();

        for (; posIdx < poses.Count; posIdx++)
        {
            InstatatePrefab(seed.PlantingFruit(), poses[posIdx]);
        }
    }

    public void Harvesting()
    {
        Debug.Log("Harvest");
        RemovingPrefab();
        isCrops = false;
        isSprout = false;
        isSeed = false;
        time = 0f;

        state = GrowState.none;
        gmInstace.GetCropsItem(seed.SeedData.cropsData);

        growSlider.gameObject.SetActive(false);
        growImage.gameObject.SetActive(false);
        posIdx = 0;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (state == GrowState.none) // 아무것도 심어지지 않았다
        {
            if (gmInstace.toolState == ToolState.trowel)
            {
                if (gmInstace.seedState == SeedState.None) popUp.SetActive(true);
                else
                {
                    seed = new SeedItem(farmSystem.GetDict(gmInstace.seedState));
                    gmInstace.SetSeedItem(seed.SeedData);
                    growDay = seed.GetGrowDay();
                    Sowing();
                }
            }
        }
        else if (gmInstace.toolState == ToolState.sickle && state == GrowState.crops) // 다 자란 상태이고 낫을 들고 있다
        {
            Debug.Log("if문 진입");
            Harvesting();
        }
        else return;
    }

    void InstatatePrefab(GameObject _prefab, GameObject _parent)
    {
        GameObject tmp = Instantiate(_prefab, _parent.transform.position, _prefab.transform.rotation);
        tmp.transform.parent = _parent.transform;
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.localScale = Vector3.one;
        plants.Add(tmp);
    }

    void RemovingPrefab()
    {
        foreach (var item in plants)
        {
            Destroy(item);
        }
        plants.Clear();
    }
}
