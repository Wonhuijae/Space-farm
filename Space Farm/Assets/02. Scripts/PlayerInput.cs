using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    // 수직 수평 축 입력
    public string moveHAxisName = "Horizontal";
    public string moveVAxisName = "Vertical";
    public string jumpKeyName = "Jump";
    public VariableJoystick joystick;

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

    void Update()
    {
#if UNITY_EDITOR
        hValue = Input.GetAxis(moveHAxisName);
        vValue = Input.GetAxis(moveVAxisName);
#else
        hValue = joystick.Horizontal;
        vValue = joystick.Vertical;
#endif
        isJump = Input.GetButton("Jump");

        isMouseDown = Input.GetMouseButton(0);

        if(isMouseDown)
        {
            rX = Input.GetAxis("Mouse X");
            rY = Input.GetAxis("Mouse Y");
        }
    }
    public bool IsPoinerOverUIObject() // UI 요소와 상호작용하는 지점인지 판단
    {
        PointerEventData eventPos = new PointerEventData(EventSystem.current);
        eventPos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();

        // 모든 UI 요소에 Raycast함
        EventSystem.current.RaycastAll(eventPos, results);

        return results.Count > 0;
    }
}
