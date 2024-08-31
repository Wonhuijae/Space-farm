using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // 카메라 정보
        // 위치 Vector3(97,1.75,101.199997)
        // 각도 Vector3(7.6321578,180,0)
        cam.transform.position = new Vector3(97, 1.75f, 101.20f);
        cam.transform.rotation = Quaternion.Euler(new Vector3(7.632f, 180f, 0f));
    }
}
