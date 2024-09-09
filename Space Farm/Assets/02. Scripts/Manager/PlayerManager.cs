using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject CameraPos;
    public Transform ridePos;
    public Transform normalPos;

    AudioSource playerAs;
    SkinnedMeshRenderer playerRD;
    GameManager gmInstance;

    public GameObject[] tools;

    void Awake()
    {
        playerAs = GetComponent<AudioSource>();
        playerRD = GetComponentInChildren<SkinnedMeshRenderer>();

        playerRD.materials[1].color= playerData.color;

        gmInstance = GameManager.Instance;
        gmInstance.onGetTracktor += Ride;
        gmInstance.onDownTracktor += GetOff;
    }

    public void Ride()
    {
        Debug.Log("On");
        playerRD.enabled = false;
        CameraPos.transform.position = ridePos.position;
        tools[4].GetComponent<ITools>().Use();
    }
    
    public void GetOff()
    {
        Debug.Log("Off");
        playerRD.enabled = true;
        CameraPos.transform.position = normalPos.position;
        tools[4].GetComponent<ITools>().UnUse();
    }
}
