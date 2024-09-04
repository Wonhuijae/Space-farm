using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SctiptableData/GoodsData", fileName = "Goods Data")]
public class GoodsData : ScriptableObject
{
    public string Name;
    public int Price;
    public string Description;
    public Sprite Image;
}
