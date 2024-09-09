using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracktor : MonoBehaviour, ITools
{
    public ToolState CurToolState()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        gameObject.SetActive(true);
    }

    public void UnUse()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        FieldCycle fc = other.GetComponent<FieldCycle>();

        if(fc != null)
        {
            if (fc.IsCrops()) fc.Harvesting();
        }
    }
}
