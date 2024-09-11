using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprilkerCollision : MonoBehaviour
{
    FarmSystem farm;

    public event Action OnTriggerOthers;
    public event Action OffTriggerOthers;

    ParticleSystem p;

    float time = 20f;

    private void Awake()
    {
        

        farm = FarmSystem.instance;

        if(gameObject.name != "Sprinkler_O") p = GetComponentInChildren<ParticleSystem>();

        if (farm != null)
        {
            OnTriggerOthers += farm.ChangeStateCollEnterSprin;
            OffTriggerOthers += farm.ChangeStateCollExitSprin;
        }
    }

    private void Update()
    {
        if(p == null ) return;

        time -= Time.deltaTime;

        if (time < 0)
        {
            p.Play();
            WateringSphere();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sprinkler"))
        {
            OnTriggerOthers?.Invoke();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sprinkler")) OffTriggerOthers?.Invoke();
    }

    void WateringSphere()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 3.25f);
        foreach(Collider c in hits)
        {
            if (c.GetComponent<FieldCycle>() != null) c.GetComponent<FieldCycle>().Watering();
        }
    }
}
