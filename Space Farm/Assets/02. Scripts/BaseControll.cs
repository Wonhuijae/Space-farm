using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControll : MonoBehaviour
{
    public OutlineShader computerOut;
    public OutlineShader projecterOut;

    public GameObject baseCamera;
    public GameObject farmCamera;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            computerOut.enabled = true;
            projecterOut.enabled = true;

            baseCamera.SetActive(true);
            farmCamera.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            computerOut.enabled = false;
            projecterOut.enabled = false;

            baseCamera.SetActive(false);
            farmCamera.SetActive(true);
        }
    }
}
