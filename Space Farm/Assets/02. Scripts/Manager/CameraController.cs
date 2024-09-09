using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    public GameObject baseCam;
    public GameObject baseVCam;
    public GameObject farmCam;
    public GameObject Roof;

    PlayerInput playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void FixedUpdate()
    {
        if (playerInput != null)
        {
            //Debug.Log($"{playerInput.rX} , {playerInput.rY}");
            //activeCamera.inputAxisValue = playerInput.rX;
            //activeCamera.m_YAxis.Value = playerInput.rY;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Roof.SetActive(false);
            baseCam.SetActive(true);
            farmCam.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Roof.SetActive(true);
            baseCam.SetActive(false);
            farmCam.SetActive(true);
        }
    }

    public void ScrollUP()
    {

    }
}
