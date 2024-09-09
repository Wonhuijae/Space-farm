using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FieldCycle fc = other.GetComponent<FieldCycle>();

        if(fc != null)
        {
            if (fc.IsCrops()) fc.Harvesting();
        }
    }
}
