using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsData : ScriptableObject // �۹� ���� ������
{
    public ItemData itemData; // �⺻ ������ ������
    public int GrowDay; // ���� �ð�
    public int SalePrice; // �ǸŰ�
    public GameObject Seed;
    public GameObject Sprout;
    public GameObject Adult;
}
