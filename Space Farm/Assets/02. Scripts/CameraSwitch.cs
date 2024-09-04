using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject baseCam;
    public GameObject basrVCam;
    public GameObject farmCam;
    public GameObject farmVCam;
    public GameObject Roof;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Roof.SetActive(false);
            baseCam.SetActive(true);
            basrVCam.SetActive(true);
            farmCam.SetActive(false);
            farmVCam.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Roof.SetActive(true);
            baseCam.SetActive(false);
            basrVCam.SetActive(false);
            farmCam.SetActive(true);
            farmVCam.SetActive(true);
        }
    }
}
