using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SctiptableData/GoodsData", fileName = "Item Data")]
public class ItemData : ScriptableObject
{
    public string Name;
    public int Price;
    public string Description; 
    public Sprite Icon_Shop;
    public Sprite Icon_Item;
    public string shortCutName;
    public ToolState toolState;
}
