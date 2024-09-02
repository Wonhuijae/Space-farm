using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentController : MonoBehaviour
{
    public Light dLight;
    float dayDuration = 60f; // 게임 내 하루 시간
    private float dayCounter = 0f;
    float lightAngle = 0f;

    // Update is called once per frame
    void Update()
    {
        TimeCycle();
    }

    void TimeCycle()
    {
        dayCounter += Time.deltaTime / dayDuration;
        if (dayCounter >= 1f) dayCounter = 0f; // 하루 지나면 초기화

        lightAngle = Mathf.Lerp(0, 360, dayCounter); // 0에서 360까지 counter만큼 증가
        dLight.transform.rotation = Quaternion.Euler(new Vector3(lightAngle - 90, 90, 0)); // z 각도, y각도는 고정, x각도 변화, -90부터 시작

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.25f); // 스카이박스 회전
    }
}
