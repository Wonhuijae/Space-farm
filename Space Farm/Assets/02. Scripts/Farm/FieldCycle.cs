using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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

    void Awake()
    {
        state = GrowState.none;

        gmInstace = GameManager.Instance;
        farmSystem = FarmSystem.instance;
        popUp = farmSystem.SeedPopup;

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t == transform || t.name == "Seeds" || t.name == "Sprout" || t.name == "Adult" || t.name == "field") continue;
            poses.Add(t.gameObject);
        }
    }

    private void Update()
    {
        if (state == GrowState.none || seed == null) return;

        time += Time.deltaTime;

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

        state = GrowState.none;
        gmInstace.GetCropsItem(seed.SeedData.cropsData);
    }

    private void OnMouseDown()
    {
        if (state == GrowState.none) // 아무것도 심어지지 않았다
        {
            if (gmInstace.toolState == ToolState.trowel)
            {
                if (gmInstace.seedState == SeedState.None) popUp.SetActive(true);
                else
                {
                    seed = new SeedItem(farmSystem.GetDict(gmInstace.seedState));
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
