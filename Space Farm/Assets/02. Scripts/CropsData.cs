using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsData : ScriptableObject // 작물 고유 데이터
{
    public ItemData itemData; // 기본 아이템 데이터
    public int GrowDay; // 성장 시간
    public int SalePrice; // 판매가
    public GameObject Seed;
    public GameObject Sprout;
    public GameObject Adult;
}
