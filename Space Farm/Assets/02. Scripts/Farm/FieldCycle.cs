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
        isSeed = true;
        state = GrowState.seed;
        for(; posIdx < 2; posIdx++)
        {
            InstatatePrefab(seed.Sowing(), poses[posIdx]);
        }
    }

    void Sprouting()
    {
        isSprout = true;
        state = GrowState.sprout;
        foreach (var item in plants)
        {
            Destroy(item);
        }
        plants.Clear();

        for (; posIdx < 5;posIdx++)
        {
            InstatatePrefab(seed.Sprouting(), poses[posIdx]);
        }
    }

    void PlantingFruit()
    {
        isCrops = true;
        state = GrowState.crops;
        foreach (var item in plants)
        {
            Destroy(item);
        }
        plants.Clear();

        for (; posIdx < poses.Count; posIdx++)
        {
            InstatatePrefab(seed.PlantingFruit(), poses[posIdx]);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(gmInstace.toolState);
        Debug.Log(gmInstace.seedState);
        if (gmInstace.toolState != ToolState.trowel || state != GrowState.none) return;
        if (gmInstace.seedState == SeedState.None) popUp.SetActive(true);
        else
        {
            Debug.Log("sowing");
            seed = new SeedItem(farmSystem.GetDict(gmInstace.seedState));
            growDay = seed.GetGrowDay();
            Sowing();
        }
    }

    void InstatatePrefab(GameObject _prefab, GameObject _parent)
    {
        GameObject tmp = Instantiate(_prefab, _parent.transform.position, _prefab.transform.rotation);
        tmp.transform.parent = _parent.transform;
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.localScale = Vector3.one;
        plants.Add(tmp);
    }
}
