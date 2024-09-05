using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 수직 수평 축 입력
    public string moveHAxisName = "Horizontal";
    public string moveVAxisName = "Vertical";
    public string jumpKeyName = "Jump";

    private UIManager UIinstance;

    // 입력 값
    public float hValue { get; private set; }
    public float vValue { get; private set; }
    public bool isJump { get; private set; }

    // 클릭
    public bool isMouseDown { get; private set; }
    public bool isMouseUp { get; private set; }
    public bool isMouseOneClick { get; private set; }

    public float rX { get; private set; }
    public float rY { get; private set; }

    void Awake()
    {
        hValue = 0f;
        vValue = 0f;

        isMouseDown = false;
        isMouseUp = false;
        isMouseOneClick = false;
    }

    private void Start()
    {
        UIinstance = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        hValue = Input.GetAxis(moveHAxisName);
        vValue = Input.GetAxis(moveVAxisName);

        isJump = Input.GetButton("Jump");

        isMouseDown = Input.GetMouseButton(0);

        if(isMouseDown)
        {
            rX = Input.GetAxis("Mouse X");
            rY = Input.GetAxis("Mouse Y");
        }
    }
}
