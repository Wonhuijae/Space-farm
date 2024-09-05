using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FieldCollison : MonoBehaviour
{
    public event Action onCollEnterOthers;
    public event Action onCollLeaveOthers;
    private FarmSystemInput FSInstance;

    private void Awake()
    {
        FSInstance = FarmSystemInput.instance;
        onCollEnterOthers += FSInstance.ChangeStateCollEnter;
        onCollLeaveOthers += FSInstance.ChangeStateCollExit;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlacedField")) onCollEnterOthers?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
         onCollLeaveOthers?.Invoke();
    }
}
