using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SctiptableData/ToolData", fileName = "Tool Data")]
public class ToolData : ItemData
{
    [SerializeField] private Sprite _icon_shop;
    [SerializeField] private Sprite _icon_shortcut;
    [SerializeField] private ToolState _toolState;
    [SerializeField] private GameObject _toolModel;
    [SerializeField] private string _shortcutname;
    
    
    public Sprite Icon_Shop => _icon_shop;
    public Sprite Icon_ShortCut => _icon_shortcut;
    public ToolState toolState => _toolState;
    public GameObject ToolModel => _toolModel;
    public string ShortcutName => _shortcutname;
    
    public int Price;
    public Tier Tier;
    public int Durability;

    public override Item CreateItem()
    {
        return new ToolItem(this);
    }
}
