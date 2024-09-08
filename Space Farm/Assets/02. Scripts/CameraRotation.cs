using UnityEngine;

public class CircularCameraMovement : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public float radius = 5f;  // 플레이어와 카메라 사이의 거리 (반지름)
    public float rotationSpeed = 50f;  // 회전 속도

    private float currentAngle = 0f;  // 현재 회전 각도

    void Update()
    {
        // A, D 입력에 따라 카메라 각도를 조정
        if (Input.GetKey(KeyCode.A))
        {
            currentAngle -= rotationSpeed * Time.deltaTime;  // 반시계 방향
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentAngle += rotationSpeed * Time.deltaTime;  // 시계 방향
        }

        // 카메라의 각도를 기준으로 X, Z 좌표를 계산하여 원형으로 이동
        float radians = currentAngle * Mathf.Deg2Rad;
        float xOffset = Mathf.Sin(radians) * radius;
        float zOffset = Mathf.Cos(radians) * radius;

        // 카메라의 새로운 위치 설정 (플레이어를 중심으로)
        Vector3 newPosition = new Vector3(player.position.x + xOffset, transform.position.y, player.position.z + zOffset);
        transform.position = newPosition;

        // 카메라가 항상 플레이어를 바라보도록 설정 (원하면 주석 해제)
        transform.LookAt(player);
    }
}