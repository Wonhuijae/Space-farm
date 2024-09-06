using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public ItemData Data { get; private set; }

    public Item(ItemData _data) => Data = _data;
}

public class ToolItem : Item, ITools
{
    public ToolData toolData { get; private set; }
    public ToolItem(ToolData _toolData) : base(_toolData)
    {
        toolData = _toolData;
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }
    public ToolState CurToolState()
    {
        return toolData.toolState;
    }
}

public class SeedItem
{

}

public class CropsItem
{

}
