using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SctiptableData/CropsData", fileName = "Crops Data")]
public class CropsData : ItemData // 작물 고유 데이터
{
    public int SalePrice; // 판매가
    public Sprite Icon_Inventory;
    public int Quantity;

    public override Item CreateItem()
    {
        return new CropsItem(this);
    }
}
