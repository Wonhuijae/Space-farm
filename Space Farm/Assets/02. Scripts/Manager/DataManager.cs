using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FieldDataList
{
    public List<FieldData> fields = new List<FieldData>();
}

public class PlayerDataList
{
    public PlayerRunTimeData info = new PlayerRunTimeData();
}

public class InventoryData
{
    public List<ItemDataJson> info = new List<ItemDataJson>();
}

[System.Serializable]
public class ItemDataJson
{
    public string code;
    public int quantity;

    public ItemDataJson(string _code, int _q)
    {
        code = _code;
        quantity = _q;
    }
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<DataManager>();
            }

            return m_instance;
        }
    }
    private static DataManager m_instance;
    private FarmSystem fmInstance;

    private GameManager gmInstance;

    public event Action<FieldDataList> OnLoadData;

    string directoryPath;
    string fieldFilePath;
    string playerFilePath;
    string inventoryFilePath;

    FieldDataList saveFieldData;
    FieldDataList loadFieldData;

    InventoryData saveInventoryData;
    InventoryData loadInventoryData;
    Dictionary<string, int> inven = new();

    PlayerDataList savePlayerData;
    PlayerDataList loadPlayerData;

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }

        fmInstance = FarmSystem.instance;
        if (fmInstance != null)
        {
            OnLoadData += fmInstance.LoadFields;
        }

        gmInstance = GameManager.Instance;

        directoryPath = Application.persistentDataPath + "/SaveData/";
        fieldFilePath = directoryPath + "SaveData.json";
        playerFilePath = directoryPath + "PlayerData.json";
        inventoryFilePath = directoryPath + "InventoryData.json";

        saveFieldData = new FieldDataList();
        loadFieldData = new FieldDataList();

        savePlayerData = new PlayerDataList();
        loadPlayerData = new PlayerDataList();

        saveInventoryData = new InventoryData();
        loadInventoryData = new InventoryData();
    }

    private void Start()
    {
        LoadPlayer();
        LoadFields();
        LoadInven();
    }

    public void SaveDataToFieldsList(FieldData _oldD, FieldData _d)
    {
        if (saveFieldData.fields.Contains(_oldD))
        {
            saveFieldData.fields[saveFieldData.fields.IndexOf(_oldD)] = _d;
        }
        else saveFieldData.fields.Add(_d);

        SaveData(saveFieldData, fieldFilePath);
    }

    public void SaveDataToInventoryList(string _code, int _q)
    {
        
        ItemDataJson item = saveInventoryData.info.Find(i => i.code == _code);

        if(item != null)
        {
            item.quantity += _q;
            inven[_code] += _q;
        }
        else
        {
            saveInventoryData.info.Add(new ItemDataJson(_code, _q));
            inven.Add(_code, _q);
        }
        
        SaveData(saveInventoryData, inventoryFilePath);
    }

    public void SaveDataToPlayerList(PlayerRunTimeData _d)
    {
        savePlayerData.info = _d;

        SaveData(savePlayerData, playerFilePath);
    }

    void SaveData<T>(T _list, string _fliepath) where T : class
    {
        if(!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string json = JsonUtility.ToJson(_list, true); // 줄바꿈 및 들여쓰기
        
        File.WriteAllText(_fliepath, json);
    }

    T LoadData<T>(string _filePath) where T : class // T는 클래스
    {
        if (!File.Exists(_filePath)) return null;

        string json = File.ReadAllText(_filePath);

        T loadData = JsonUtility.FromJson<T>(json);

        return loadData;
    }

    void LoadFields()
    {
        loadFieldData = LoadData<FieldDataList>(fieldFilePath);

        if (loadFieldData != null)
        {
            saveFieldData.fields = loadFieldData.fields;
            OnLoadData?.Invoke(loadFieldData);
        }
    }

    void LoadPlayer()
    {
        loadPlayerData = LoadData<PlayerDataList>(playerFilePath);

        if (loadPlayerData != null)
        {
            savePlayerData.info = loadPlayerData.info;
        }
        else
        {
            SaveDataToPlayerList(gmInstance.Init());
        }
    }

    void LoadInven()
    {
        loadInventoryData = LoadData<InventoryData>(inventoryFilePath);

        if(loadInventoryData != null)
        {
            saveInventoryData.info = loadInventoryData.info;
            foreach(var l in saveInventoryData.info)
            {
                inven.Add(l.code, l.quantity);
            }
        }
    }

    public PlayerRunTimeData GetPlayerInfo()
    {
        LoadPlayer();
        return savePlayerData.info;
    }

    private void OnApplicationQuit()
    {
        SaveData<FieldDataList>(saveFieldData, fieldFilePath);
        SaveData<PlayerDataList>(savePlayerData, playerFilePath);
        SaveData<InventoryData>(saveInventoryData, inventoryFilePath);
    }

    public int getQuantity(string _code)
    {
        if(inven.ContainsKey(_code)) return inven[_code];
        else
        {
            saveInventoryData.info.Add(new ItemDataJson(_code, 0));
            inven.Add(_code, 0);
            return 0;
        }
    }
}
