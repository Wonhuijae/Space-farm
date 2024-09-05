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
            return _money;
        }
        private set
        {
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
    public int exp // °æÇèÄ¡
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int _earnExp)
    {
        exp += _earnExp;
    }

    public void ChangeTool(ToolState _toolState)
    {
        toolState = _toolState;
    }
}
