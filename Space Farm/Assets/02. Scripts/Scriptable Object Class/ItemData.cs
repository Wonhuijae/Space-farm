using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "SctiptableData/GoodsData", fileName = "Item Data")]
public abstract class ItemData : ScriptableObject
{
    [SerializeField] private string _code;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _price;

    public string Code => _code;
    public string Name => _name;
    public int Price => _price;
    public string Description => _description;

    public abstract Item CreateItem();
}
