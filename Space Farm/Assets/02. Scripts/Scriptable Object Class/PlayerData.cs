using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SctiptableData/PlayerData", fileName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public int money;
    public Color color;
    public int level;
    public int exp;
    public int maxExp;

    public ToolState ToolState;
}

[System.Serializable]
public class PlayerRunTimeData
{
    public int money;
    public ColorToSeriallize color;
    public int level;
    public int exp;
    public int maxExp;

    public void Init(PlayerData _pData)
    {
        money= _pData.money;
        color = new ColorToSeriallize(_pData.color);
        level= _pData.level;
        exp= _pData.exp;
        maxExp= _pData.maxExp;
    }
}

[System.Serializable]
public class ColorToSeriallize
{
    public float r;
    public float g;
    public float b;
    public float a;

    public ColorToSeriallize(Color _color)
    {
        r = _color.r;
        g = _color.g;
        b = _color.b;
        a = _color.a;
    }

    public Color ToColor()
    {
        return new Color(r, g, b, a);
    }
}