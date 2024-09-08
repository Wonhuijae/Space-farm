using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    public float rotationSpeed = 100f;

    private void OnEnable()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0)) return;

        float hInput = Input.GetAxis("Mouse X");

        if(hInput != 0f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, hInput * rotationSpeed, 0));

            //var oribitalTransposer = vCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            //if(oribitalTransposer != null )
            //{
            //    oribitalTransposer.m_XAxis.Value += hInput * rotationSpeed * Time.deltaTime;
            //}
        } 
        
        
    }
}
