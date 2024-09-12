using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControll : MonoBehaviour
{
    public OutlineShader computerOut;
    public OutlineShader projecterOut;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            computerOut.enabled = true;
            projecterOut.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            computerOut.enabled = false;
            projecterOut.enabled = false;
        }
    }
}
