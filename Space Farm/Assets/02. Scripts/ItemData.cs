using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SctiptableData/GoodsData", fileName = "Item Data")]
public class ItemData : ScriptableObject
{
    public string Name; // 이름
    public int Price; // 가격
    public string Description; // 설명
    public Sprite Icon_Shop; // 상점용 아이콘
    public Sprite Icon_Item; // 아이템/단축바용 아이콘
}
