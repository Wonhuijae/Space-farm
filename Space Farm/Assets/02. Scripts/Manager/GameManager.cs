using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = FindObjectOfType<GameManager>();
            }
            return m_Instance;
        }
    }
    private static GameManager m_Instance;

    public event Action<ToolData> onPurchasedItemTool;
    public event Action<SeedData> onPurchasedItemSeed;
    public event Action<CropsData> onGetItemCrops;

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private ToolData[] toolData;
    [SerializeField]
    private SeedData[] seedData;

    public int money
    {
        get
        {
            _money = playerData.money;
            return _money;
        }
        private set
        {
            _money = value;
            playerData.money = value;
        }
    }
    private int _money;
    public int level
    {
        get
        {
            _level = playerData.level;
            return _level;
        }
        private set
        {
            _level = value;
        }
    }
    private int _level;
    public int exp
    {
        get
        {
            _exp = playerData.exp;
            return _exp;
        }
        private set
        {
            if (value >= maxExp)
            {
                level++;
                value -= maxExp;
                maxExp = (int)(1.05f * maxExp);
            }
            _exp = value;
        }
    }
    private int _exp;
    public int maxExp { get; private set; }
    public ToolState toolState
    {
        get
        {
            _toolState = playerData.ToolState;
            return _toolState;
        }
        private set
        {
            playerData.ToolState = value;
        }
    }
    private ToolState _toolState;

    private UIManager uiInstance;

    void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
        }

        maxExp = 100;

        uiInstance = FindObjectOfType<UIManager>();

        Debug.Log($"{level}, {exp}, {maxExp}, {money}");
    }

    public void GetExp(int _earnExp)
    {
        exp += _earnExp;
    }

    public SeedData[] GetSeedData()
    {
        return seedData;
    }

    public ToolData[] GetToolData()
    {
        return toolData;
    }

    public void ChangeTool(ToolState _toolState)
    {
        toolState = _toolState;
        playerData.ToolState = toolState;

        Debug.Log(toolState);
    }

    public void TryToPurchaseSeed(SeedData _seed)
    {
        if (money < _seed.Price) return;
        else
        {
            GetSeedItem(_seed);
        }
    }

    public void GetSeedItem(SeedData _seed)
    {
        if(Array.Exists(seedData, e => e == _seed))
        {
            _seed.Quantity += 10;
        }

        if (onPurchasedItemSeed != null) onPurchasedItemSeed?.Invoke(_seed);
    } 

    public void TryToPurchaseTool(ToolData _Tool)
    {
        if (money < _Tool.Price) return;
        else
        {
            GetToolItem(_Tool);
        }
    }

    private void GetToolItem(ToolData _tool)
    {
        if (Array.Exists(toolData, e => e == _tool)) 
        {
            if (onPurchasedItemTool != null) onPurchasedItemTool.Invoke(_tool);
        }
    }
}
