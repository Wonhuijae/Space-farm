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
    private DataManager dInstance;

    public event Action<ToolData> onPurchasedItemTool;
    public event Action<SeedData> onPurchasedItemSeed;
    public event Action<CropsData> onGetItemCrops;
    public event Action<int> onToolsOn;
    public event Action onAllToolsOff;

    public GameObject[] OnTools;

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private ToolData[] toolData;
    [SerializeField]
    private SeedData[] seedData;
    [SerializeField]
    private CropsData[] cropsData;

    private bool _WaitForExit = false;

    public int money
    {
        get
        {
            return _money;
        }
        private set
        {
            _money = value;
            runtimeData.money = value;
            uiInstance.GeneralUISetting();
            PlayerSettingSave();
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
            runtimeData.level = value;
            uiInstance.GeneralUISetting();
            PlayerSettingSave();
        }
    }
    private int _level;
    public int exp
    {
        get
        {
            return _exp;
        }
        private set
        {
            _exp = value;

            if (_exp > maxExp)
            {
                level++;
                _exp -= maxExp;
                maxExp = (int)(1.05f * maxExp);
            }

            runtimeData.exp = _exp;
            uiInstance.GeneralUISetting();
            PlayerSettingSave();
        }
    }
    private int _exp;
    public int maxExp
    {
        get
        {
            return _maxExp;
        }
        private set
        {
            _maxExp = value;
            runtimeData.maxExp = value;
            uiInstance.GeneralUISetting();
            PlayerSettingSave();
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

    private PlayerRunTimeData runtimeData = new();

    void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
        }

        uiInstance = UIManager.instance;
        dInstance = DataManager.instance;

        toolState = ToolState.None;

        runtimeData = dInstance.GetPlayerInfo();

        money = runtimeData.money;
        level = runtimeData.level;
        maxExp = runtimeData.maxExp;
        exp = runtimeData.exp;
        
        ChangeTool(toolState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_WaitForExit == false)
            {
                showAndroidToastMessage("종료하려면 한 번 더 누르세요");
                StartCoroutine(WaitInput());
            }
            else
            {
                Application.Quit();
            }
        }
    }

    private IEnumerator WaitInput()
    {
        _WaitForExit = true;
        yield return new WaitForSecondsRealtime(2.5f);
        _WaitForExit = false;
    }

    private void showAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }

    public void PlayerSettingSave()
    {
        dInstance.SaveDataToPlayerList(runtimeData);
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
        switch(_toolState)
        {
            case ToolState.None:
                onAllToolsOff?.Invoke();
                break;
            case ToolState.hoe:
                onToolsOn?.Invoke(0);
                break;
            case ToolState.trowel:
                onToolsOn?.Invoke(1);
                break;
            case ToolState.watercan:
                onToolsOn?.Invoke(2);
                break;
            case ToolState.sickle:
                onToolsOn?.Invoke(3);
                break;
            case ToolState.traktor:
                onToolsOn?.Invoke(4);
                break;
            case ToolState.sprinkler:
                onToolsOn?.Invoke(5);
                break;
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
            uiInstance.GeneralUISetting();
        }
    }

    public void GetSeedItem(SeedData _seed)
    {
        if(Array.Exists(seedData, e => e == _seed))
        {
            dInstance.SaveDataToInventoryList(_seed.Code, 10);
            money -= _seed.Price;
        }
    }

    public void SetSeedItem(SeedData _seed)
    {
        dInstance.SaveDataToInventoryList(_seed.Code, -1);
    }
    
    public void GetCropsItem(CropsData _crops)
    {
        dInstance.SaveDataToInventoryList(_crops.Code, 1);
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
        money += salesPrice;
        dInstance.SaveDataToInventoryList(_saleCrops.Code, -_salesQ);
    }

    private void GetToolItem(ToolData _tool)
    {
        if (Array.Exists(toolData, e => e == _tool)) 
        {
            if (onPurchasedItemTool != null) onPurchasedItemTool.Invoke(_tool);
        }
    }

    public void SetColor(Color _c)
    {
        runtimeData.color = new ColorToSeriallize(_c);
        dInstance.SaveDataToPlayerList(runtimeData);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public PlayerRunTimeData Init()
    {
        runtimeData.Init(playerData);
        runtimeData.color = new ColorToSeriallize(playerData.color);

        return runtimeData;
    }

    public int GetQuantity(string _code)
    {
        return dInstance.getQuantity(_code);
    }
}