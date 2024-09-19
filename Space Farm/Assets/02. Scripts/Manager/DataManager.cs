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
    public PlayerRunTimeData info = new();
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

    public event Action<FieldDataList> OnLoadData;

    string directoryPath;
    string fieldFilePath;
    string playerFilePath;

    FieldDataList saveFieldData;
    FieldDataList loadFieldData;

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

        directoryPath = Application.persistentDataPath + "/SaveData/";
        fieldFilePath = directoryPath + "SaveData.json";
        playerFilePath = directoryPath + "PlayerData.json";

        saveFieldData = new FieldDataList();
        loadFieldData = new FieldDataList();

        savePlayerData = new PlayerDataList();
        loadPlayerData = new PlayerDataList();
    }

    private void Start()
    {
        LoadPlayer();
        LoadFields();
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

    public void SaveDataToPlayerList(PlayerRunTimeData _d)
    {
        savePlayerData.info = _d;

        SaveData(savePlayerData, playerFilePath);
    }

    public void SaveData<T>(T _list, string _fliepath) where T : class
    {
        if(!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string json = JsonUtility.ToJson(_list, true); // 줄바꿈 및 들여쓰기
        
        File.WriteAllText(_fliepath, json);
        Debug.Log(_fliepath) ;
    }

    public T LoadData<T>(string _filePath) where T : class // T는 클래스
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
    }

    private void OnApplicationQuit()
    {
        
    }
}
