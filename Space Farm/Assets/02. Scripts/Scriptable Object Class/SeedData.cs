using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SctiptableData/SeedData", fileName = "Seed Data")]
public class SeedData : ItemData
{
    public Sprite Icon_Shop;
    public Sprite Icon_Inventory;
    public GameObject Seed;
    public GameObject Sprout;
    public GameObject Adult;
    public int GrowDay;
    public int Quantity;

    public override Item CreateItem()
    {
        throw new System.NotImplementedException();
    }
}
