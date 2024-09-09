using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SctiptableData/CropsData", fileName = "Crops Data")]
public class CropsData : ItemData // �۹� ���� ������
{
    public int SalePrice; // �ǸŰ�
    public Sprite Icon_Inventory;
    public int Quantity;

    public override Item CreateItem()
    {
        return new CropsItem(this);
    }
}
