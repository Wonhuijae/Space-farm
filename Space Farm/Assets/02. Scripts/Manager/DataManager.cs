using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FieldDataList
{
    public List<FieldData> fields = new List<FieldData>();
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
    string filePath;

    FieldDataList saveFieldData;
    FieldDataList LoadFieldData;

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
        filePath = directoryPath + "SaveData.json";

        saveFieldData = new FieldDataList();
        LoadFieldData = new FieldDataList();
    }

    private void Start()
    {
        LoadData();
    }

    public void SaveDataToList(FieldData _oldD, FieldData _d)
    {
        if (saveFieldData.fields.Contains(_oldD))
        {
            saveFieldData.fields[saveFieldData.fields.IndexOf(_oldD)] = _d;
        }
        else saveFieldData.fields.Add(_d);

        SaveData();
    }

    public void SaveData()
    {
        if(!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string json = JsonUtility.ToJson(saveFieldData, true); // 줄바꿈 및 들여쓰기
        File.WriteAllText(filePath, json);
    }

    void LoadData()
    {
        if (!File.Exists(filePath)) return;

        string json = File.ReadAllText(filePath);

        LoadFieldData = JsonUtility.FromJson<FieldDataList>(json);

        if (LoadFieldData != null)
        {
            saveFieldData.fields = LoadFieldData.fields;
            OnLoadData?.Invoke(LoadFieldData);
        }
    }
}
