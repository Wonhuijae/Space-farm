using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRotation : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    public float sesitivity = 1.0f;
    public float rotateSpeed = 5f;

    private Vector3 lastMousePosition;


    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void FixedUpdate()
    {
        if(vCam != null && !EventSystem.current.IsPointerOverGameObject())
        {
            // 마우스 입력 처리
            if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시작
            {
                lastMousePosition = Input.mousePosition; // 마우스 위치 저장
            }
            else if (Input.GetMouseButton(0)) // 마우스를 누르고 있을 때
            {
                Vector3 delta = Input.mousePosition - lastMousePosition; // 이전 위치와 현재 위치의 차이
                lastMousePosition = Input.mousePosition; // 현재 위치를 업데이트

                // 카메라 회전 계산
                float rotationX = delta.y * rotateSpeed * Time.deltaTime; // 상하 회전
                float rotationY = delta.x * rotateSpeed * Time.deltaTime; // 좌우 회전

                // 카메라 회전 적용
                transform.Rotate(Vector3.up, -rotationY, Space.World); // Y축(좌우 회전)
                transform.Rotate(Vector3.right, rotationX, Space.Self); // X축(상하 회전)
            }
        }
    }
}