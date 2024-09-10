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
    public event Action onGetTracktor;
    public event Action onDownTracktor;

    private bool isRide = false;

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private ToolData[] toolData;
    [SerializeField]
    private SeedData[] seedData;
    [SerializeField]
    private CropsData[] cropsData;

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
            playerData.level = value;
            uiInstance.GeneralUISetting();
        }
    }
    private int _level;
    public int exp
    {
        get
        {
            return playerData.exp;
        }
        private set
        {
            playerData.exp = value;

            if (playerData.exp > maxExp)
            {
                level++;
                playerData.exp -= maxExp;
                maxExp = (int)(1.05f * maxExp);
            }
            uiInstance.GeneralUISetting();
            Debug.Log($" {exp}/{maxExp}");
        }
    }
    //private int _exp;
    public int maxExp
    {
        get
        {
            _maxExp = playerData.maxExp;
            return _maxExp;
        }
        private set
        {
            playerData.maxExp = value;
            _maxExp = value;
            uiInstance.GeneralUISetting();
        }
    }
    private int _maxExp;
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
    public SeedState seedState { get; private set; }

    private UIManager uiInstance;

    void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
        }

        uiInstance = UIManager.instance;
        toolState = ToolState.None;
    }

    public SeedData[] GetSeedData()
    {
        return seedData;
    }

    public ToolData[] GetToolData()
    {
        return toolData;
    }

    public CropsData[] GetCropsData()
    {
        return cropsData;
    }

    public void ChangeTool(ToolState _toolState)
    {
        if (_toolState == ToolState.traktor && !isRide)
        {
            isRide = true;
            onGetTracktor?.Invoke();
        }
        else if (_toolState == ToolState.None && isRide)
        {
            isRide = false;
            onDownTracktor?.Invoke();
        }
        toolState = _toolState;
        playerData.ToolState = toolState;
    }

    public void ChangeSeed(SeedState _seedState)
    {
        seedState = _seedState;
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

    public void SetSeedItem(SeedData _seed)
    {
        if (Array.Exists(seedData, e => e == _seed) && _seed.Quantity > 0)
        {
            _seed.Quantity--;
        }
    }
    
    public void GetCropsItem(CropsData _crops)
    {
        _crops.Quantity++;
        exp += 20;
    }

    public void TryToPurchaseTool(ToolData _Tool)
    {
        if (money < _Tool.Price) return;
        else
        {
            GetToolItem(_Tool);
        }
    }

    public void SendCrops(int _salesQ, int salesPrice, CropsData _saleCrops)
    {
        playerData.money += salesPrice;
        _saleCrops.Quantity -= _salesQ;
    }

    private void GetToolItem(ToolData _tool)
    {
        if (Array.Exists(toolData, e => e == _tool)) 
        {
            if (onPurchasedItemTool != null) onPurchasedItemTool.Invoke(_tool);
        }
    }
}