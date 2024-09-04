using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentController : MonoBehaviour
{
    public Light dLight;
    float dayDuration = 4320f; // 게임 내 하루 시간 : 3초 1분, 180초 1시간,  4320초 하루
    int inGameM;
    int inGameH;
    private float dayCounter
    {
        get
        {
            return _dayCounter;
        }
        set
        {
            _dayCounter = value;
            inGameM = (int)(_dayCounter * dayDuration / 3);
            inGameH = inGameM / 60;
        }
    }
    private float _dayCounter = 0;
    float lightAngle = 0f;
    private UIManager UIinstance;
    event Action<int, int> onChangeTime;

    private void Awake()
    {
        UIinstance = FindAnyObjectByType<UIManager>();
        if (UIinstance != null) onChangeTime += UIinstance.SetTime;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCycle();
    }

    void TimeCycle()
    {
        dayCounter += Time.deltaTime / dayDuration;
        if (dayCounter >= 1f) dayCounter = 0f; // 하루 지나면 초기화
        onChangeTime?.Invoke(inGameH, inGameM % 60);

        //lightAngle = Mathf.Lerp(0, 360, dayCounter); // 0에서 360까지 counter만큼 증가
        //dLight.transform.rotation = Quaternion.Euler(new Vector3(lightAngle - 90, 90, 0)); // z 각도, y각도는 고정, x각도 변화, -90부터 시작

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.25f); // 스카이박스 회전
    }
}
