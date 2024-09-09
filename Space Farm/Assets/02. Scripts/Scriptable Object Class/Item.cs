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

    public void UnUse()
    {
        throw new System.NotImplementedException();
    }
}

public class SeedItem : Item, ISeeds
{
    public SeedData SeedData { get; private set; }

    public SeedItem(SeedData _data) : base(_data)
    {
        SeedData = _data; // 씨앗 데이터를 갖고 있게 된다
    }

    public int GetGrowDay()
    {
        return SeedData.GrowDay;
    }

    public GameObject Sowing()
    {
        return SeedData.Seed;
    }

    public GameObject Sprouting()
    {
        return SeedData.Sprout;
    }

    public GameObject PlantingFruit()
    {
        return SeedData.Adult;
    }
}

public class CropsItem : Item
{
    public CropsData cropsData { get; private set; }

    public CropsItem(CropsData _data) : base(_data)
    {
        cropsData = _data;
    }
}
