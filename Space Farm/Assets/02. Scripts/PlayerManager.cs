using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerData playerData;
    AudioSource playerAs;
    SkinnedMeshRenderer playerRD;

    // Start is called before the first frame update
    void Awake()
    {
        playerAs = GetComponent<AudioSource>();
        playerRD = GetComponentInChildren<SkinnedMeshRenderer>();

        playerRD.materials[1].color= playerData.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
