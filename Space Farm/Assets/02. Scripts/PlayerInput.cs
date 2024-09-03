using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 수직 수평 축 입력
    public string moveHAxisName = "Horizontal";
    public string moveVAxisName = "Vertical";
    public string jumpKeyName = "Jump";

    // 입력 값
    public float hValue { get; private set; }
    public float vValue { get; private set; }
    public bool isJump { get; private set; }

    // 클릭
    public bool isMouseDown { get; private set; }
    public bool isMouseUp { get; private set; }
    public bool isMouseOneClick { get; private set; }

    // 드래그
    public bool isMouseDrag { get; private set; }

    // 마우스오버
    public bool isMouseEnter { get; private set; }
    public bool isMouseOver { get; private set; }
    public bool isMouseExit { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        hValue = 0f;
        vValue = 0f;

        isMouseDown = false;
        isMouseUp = false;
        isMouseOneClick = false;
  
        isMouseDrag = false;

        isMouseEnter = false;
        isMouseOver = false;
        isMouseExit = false;
    }

    void Update()
    {
        hValue = Input.GetAxis(moveHAxisName);
        vValue = Input.GetAxis(moveVAxisName);

        isJump = Input.GetButton("Jump");
    }

    private void OnMouseDown() // 마우스 왼쪽 클릭 시 한 번
    {
        isMouseDown = true;
        isMouseDrag = true;
    }

    private void OnMouseUp() // 마우스 왼쪽 클릭 뗄 때 한 번
    {
        if(isMouseDrag) isMouseDrag = false;

        isMouseUp = false;
    }

    private void OnMouseDrag() // 왼쪽 버튼 드래그 시 매 프레임
    {
        isMouseDrag = true;
    }

    private void OnMouseEnter() // 게임 오브젝트의 콜라이더에 진입할 때 한 번
    {
        isMouseEnter = true;
    }
    private void OnMouseOver() // 게임 오브젝트의 콜라이더 위에 머무르는 동안 매 프레임
    {
        isMouseOver = true;
    }

    private void OnMouseExit() // 게임 오브젝트의 콜라이더에서 벗어날 때 한 번
    {
        isMouseExit = true;
    }

    private void OnMouseUpAsButton() // 클릭이 드래그 없이 한 번의 클릭으로 간주될 때 호출, 클릭 시작 위치와 종료 위치가 같을 경우
    {
        isMouseOneClick = true;
    }
}
