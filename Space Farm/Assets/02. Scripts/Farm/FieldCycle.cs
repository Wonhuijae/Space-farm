using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

[System.Serializable]
public class FieldData
{
    public GrowState state;
    public SeedState seed;

    public float time;
    public bool isSeed;
    public bool isSprout;
    public bool isCrops;
    public bool isWatered;

    public int posIdx;

    public Vector3ToSeriallize pos;
}

[System.Serializable]
public enum GrowState
{
    none,
    seed,
    sprout,
    crops
}

[System.Serializable]
public class Vector3ToSeriallize
{
    public float x;
    public float y;
    public float z;

    public Vector3ToSeriallize(Vector3 _v)
    {
        x = _v.x;
        y = _v.y;
        z = _v.z;
    }

    public Vector3 toVector3()
    {
        return new Vector3(x, y, z);
    }
}

public class FieldCycle : MonoBehaviour
{
    GrowState state;

    private GameObject popUp;
    public SeedItem seed;

    private int growDay;
    float time = 0;
    bool isSeed = false;
    bool isSprout = false ;
    bool isCrops = false;
    bool isWatered= false ;
    FieldData saveData;

    GameManager gmInstace;
    FarmSystem farmSystem;
    PlayerManager plInstance;

    List<GameObject> poses = new();
    List<GameObject> plants = new();
    private int posIdx = 0;
    private Image growImage;
    private Slider growSlider;

    Vector3 FXPos;

    void Awake()
    {
        state = GrowState.none;

        gmInstace = GameManager.Instance;
        farmSystem = FarmSystem.instance;
        plInstance = PlayerManager.instance;
        popUp = farmSystem.SeedPopup;

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t == transform || t.name == "Seeds" || t.name == "Sprout" || t.name == "Adult" || t.name == "field") continue;
            if (t.GetComponent<RectTransform>() != null)
            {
                if (t.GetComponent<Image>() != null && t.name == "Image")
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
        saveData = new();

        FXPos = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
        StartCoroutine(AutoSave());
    }

    private void Update()
    {
        if (state == GrowState.crops || seed == null || state == GrowState.none)
        {
            return;
        }

        time += Time.deltaTime;
        growSlider.value = time;

        if (time > growDay / 3 && state == GrowState.seed && !isSprout) Sprouting();
        if (time > growDay && state == GrowState.sprout && !isCrops) PlantingFruit();
    }

    IEnumerator AutoSave()
    {
        while(true)
        {
            yield return new WaitForSeconds(20f);
            SaveDataStart();
        }
    }

    private void OnMouseDown()
    {
#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
#endif

        if (state == GrowState.none) // 아무것도 심어지지 않았다
        {
            if (gmInstace.toolState == ToolState.trowel)
            {
                if (gmInstace.seedState == SeedState.None) popUp.SetActive(true);
                else
                {
                    plInstance.GetAnim().SetTrigger("Trowel");
                    Init(farmSystem.GetDict(gmInstace.seedState));
                    gmInstace.SetSeedItem(seed.SeedData);
                    Destroy(Instantiate(farmSystem.VFXs[1], FXPos, Quaternion.identity), 3f);
                    Sowing();
                }
            }
        }
        else if (gmInstace.toolState == ToolState.watercan)
        {
            plInstance.GetAnim().SetTrigger("Watering");
            plInstance.Watercan.Play();
            farmSystem.audioSource.PlayOneShot(farmSystem.wateringClip);
            Watering();
        }
        else if (gmInstace.toolState == ToolState.sickle && state == GrowState.crops) // 다 자란 상태이고 낫을 들고 있다
        {
            Harvesting();
            plInstance.GetAnim().SetTrigger("Sickle");
        }
        else return;
    }

    void Sowing()
    {
        Destroy(Instantiate(farmSystem.VFXs[4], FXPos, Quaternion.identity), 3f);

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
        Destroy(Instantiate(farmSystem.VFXs[4], FXPos, Quaternion.identity), 3f);
        state = GrowState.sprout;
        isSeed = false;
        isSprout = true;
        RemovingPrefab();

        for (; posIdx < 5; posIdx++) 
        {
            InstatatePrefab(seed.Sprouting(), poses[posIdx]);
        }
    }

    void PlantingFruit()
    {
        Destroy(Instantiate(farmSystem.VFXs[4], FXPos, Quaternion.identity), 3f);
        state = GrowState.crops;
        isSprout = false;
        isCrops = true;
        
        RemovingPrefab();

        for (; posIdx < poses.Count; posIdx++)
        {
            InstatatePrefab(seed.PlantingFruit(), poses[posIdx]);
        }
    }

    public void Watering()
    {
        if (isWatered || isCrops || !isSeed) return;
        Destroy(Instantiate(farmSystem.VFXs[2], FXPos, Quaternion.identity), 3f);
        time += 15f;
        isWatered = true;
    }
    public void Harvesting()
    {
        Destroy(Instantiate(farmSystem.VFXs[3], FXPos, Quaternion.identity), 3f);
        RemovingPrefab();
        
        isCrops = false;
        isSprout = false;
        isSeed = false;
        isWatered = false;
        time = 0f;

        state = GrowState.none;
        gmInstace.GetCropsItem(seed.SeedData.cropsData);

        growSlider.gameObject.SetActive(false);
        growImage.gameObject.SetActive(false);
        posIdx = 0;
        seed = null;
    }

    public bool IsCrops()
    {
        return isCrops;
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

    public void SaveDataTrigger()
    {
        SaveDataStart();
    }

    void SaveDataStart()
    {
        FieldData oldData = new();

        if (saveData != null)
        {
            oldData = saveData;
        }

        saveData.state = state;
        saveData.pos = new Vector3ToSeriallize(transform.position);

        if (seed != null)
        {
            saveData.time = time;
            if (seed.SeedData != null)
            {
                saveData.seed = seed.SeedData.seedState;
            }
            else
            {
                saveData.seed = SeedState.None;
            }
            saveData.isSeed = isSeed;
            saveData.isSprout = isSprout;
            saveData.isCrops = isCrops;
            saveData.isWatered = isWatered;

            saveData.posIdx = posIdx;
        }

        DataManager.instance.SaveDataToFieldsList(oldData, saveData);
    }

    public void GetFieldData(FieldData f)
    {
        saveData = f;
        LoadData();
    }

    void LoadData()
    {
        if(saveData != null)
        {
            state = saveData.state;
            transform.position = saveData.pos.toVector3();
        }
        
        time = saveData.time;
        if (saveData.seed != SeedState.None && state != GrowState.none) // 아무것도 심어져 있지 않고 상태가 none이면
        {
            Init(farmSystem.GetDict(saveData.seed));
            growSlider.gameObject.SetActive(true);
            growImage.gameObject.SetActive(true);
        }
        else
        {
            growSlider.gameObject.SetActive(false);
            growImage.gameObject.SetActive(false);
        }

        isSeed = saveData.isSeed;
        isSprout = saveData.isSprout;
        isCrops = saveData.isCrops;
        isWatered = saveData.isWatered;

        posIdx = saveData.posIdx;

        switch(state)
        {
            case GrowState.seed:
                posIdx -= 2;
                Sowing();
                break;
            case GrowState.sprout:
                posIdx -= 3;
                Sprouting();
                break;
            case GrowState.crops:
                posIdx -= 3;
                PlantingFruit();
                break;
        }
    }
    
    void Init(SeedData _s)
    {
        seed = new SeedItem(_s);
        growDay = seed.GetGrowDay();
        
        growImage.sprite = seed.SeedData.Icon_Shop;
        growSlider.maxValue = growDay;

        if (state == GrowState.crops)
        {
            growSlider.value = growDay;
            time = 0f;
        }
    }
}
