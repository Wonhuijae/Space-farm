using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance;

    [SerializeField]
    private PlayerData playerData;

    public int money
    {
        get
        {
            _money = playerData.money;
            return _money;
        }
        private set
        {
            playerData.money = value;
            _money = value;
        }
    }
    private int _money;
    public int level
    {
        get
        {
            return _level;
        }
        private set
        {
            _level = value;
        }
    }
    private int _level;
    public int exp // 경험치
    {
        get
        {
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
    private int maxExp;
    public ToolState toolState { get; private set; }
    private ToolState _toolState;

    private Dictionary<ToolData, int> toolInven = new();
    private Dictionary<SeedData, int> seedInven = new();
    private Dictionary<CropsData, int> cropsInven = new();

    public event Action onPurchased;
    private UIManager uiInstance;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<GameManager>();
        }

        _money = playerData.money;
        _level = playerData.level;
        _exp = playerData.exp;
        _toolState = playerData.ToolState;

        maxExp = 100;

        uiInstance = FindObjectOfType<UIManager>();
    }

    public void GetExp(int _earnExp)
    {
        exp += _earnExp;
    }

    public void ChangeTool(ToolState _toolState)
    {
        toolState = _toolState;
        playerData.ToolState = toolState;

        Debug.Log(toolState);
    }

    public void TryToPurchaseSeed(SeedData _seed)
    {
        Debug.Log(_seed.Code);
        if(money > _seed.Price)
        {
            if (seedInven.ContainsKey(_seed))
            {
                Debug.Log(seedInven[_seed]);
                seedInven[_seed] += 10;
                Debug.Log(seedInven[_seed]);
                uiInstance.GetItem(_seed);

                playerData.money -= _seed.Price;
                Debug.Log($"{_seed.Code} {seedInven[_seed]}");
            }
            else
            {
                seedInven.Add(_seed, 10);
                Debug.Log($"{_seed.Code} {seedInven[_seed]}");
            }
            
        }
        else
        {
            Debug.Log("잔액 부족");
        }
    }
    
    public void TryToPurchaseTool(ToolData _Tool)
    {
        
    }

    public int GetItem(SeedData _seed)
    {
        return seedInven[_seed];
    }
}
