using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrops
{
    int GetGrowDay();
    public void Grow();
    void Harvest();
}
