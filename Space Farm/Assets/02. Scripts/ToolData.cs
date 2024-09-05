using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SctiptableData/ToolData", fileName = "Tool Data")]
public class ToolData : ScriptableObject
{
    public string Name;
    public string Description;
    public Image Icon_Shop;
    public Image Icon_ShortCut;
    public ToolState toolState;
    public GameObject tool;
    public ITools ToolComponent;
}
