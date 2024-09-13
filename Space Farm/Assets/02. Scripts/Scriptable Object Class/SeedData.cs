using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "SctiptableData/SeedData", fileName = "Seed Data")]
public class SeedData : ItemData
{
    [SerializeField] private Sprite _icon_shop;
    [SerializeField] private Sprite _icon_inventory;
    [SerializeField] private GameObject _seed;
    [SerializeField] private GameObject _sprout;
    [SerializeField] private GameObject _adult;
    [SerializeField] private int _growday;
    [SerializeField] private int _price;
    [SerializeField] private SeedState _seedstate;
    [SerializeField] private CropsData _cropsdata;


    public Sprite Icon_Shop => _icon_shop;
    public Sprite Icon_Inventory => _icon_inventory;
    public GameObject Seed => _seed;
    public GameObject Sprout => _sprout;
    public GameObject Adult => _adult;
    public int GrowDay => _growday;
    public int Quantity;
    public int Price => _price;
    public SeedState seedState => _seedstate;
    public CropsData cropsData => _cropsdata;

    public override Item CreateItem()
    {
        throw new System.NotImplementedException();
    }
}
