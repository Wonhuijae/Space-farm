using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    public Camera cam;
    public GameObject computerPanel;
    public GameObject playPanel;
    

    void OnMouseDown()
    {
        playPanel.SetActive(false);
        computerPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playPanel.SetActive(true);
            computerPanel.SetActive(false);
        }
    }
}
