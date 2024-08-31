using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public AudioClip openClip;
    public AudioClip closeClip;

    private float openDistance = 0.88f;
    private float doorSpeed = 0.5f;
    private float waitCount = 3f;
    private Vector3 leftClosePosition;
    private Vector3 rightClosePosition;
    private Vector3 leftOpenPosition;
    private Vector3 rightOpenPosition;
    private Vector3 leftCurPosition;
    private Vector3 rightCurPosition;

    private Status doorStatus { get; set; }

    private AudioSource doorAS;

    enum Status
    {
        open,
        close, 
        moving,
    }

    // Start is called before the first frame update
    void Awake()
    {
        doorAS = GetComponent<AudioSource>();

        doorStatus = Status.close;
        leftClosePosition = Vector3.zero;
        rightClosePosition = Vector3.zero;
        leftOpenPosition = new Vector3(0, 0, -openDistance);
        rightOpenPosition = new Vector3(0, 0, openDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log(doorStatus);

            switch(doorStatus)
            {
                case Status.open: // 열린 상태면 대기 코루틴을 중지한다
                    if(waitRoutine != null) StopCoroutine(waitRoutine);
                    break;

                case Status.close:
                    doorStatus = Status.moving;// 닫힌 상태에서 들어오면 문을 열고 움직이는 상태로 바꾼다
                    StartCoroutine(OpenDoor());
                    break;

                case Status.moving: // 움직이는 중이면 대기를 중지하고 즉시 다시 연다
                    if (waitRoutine != null) StopCoroutine(waitRoutine);
                    if (closeRoutine != null) StopCoroutine(closeRoutine);
                    StartCoroutine(OpenDoor());
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
         {
            if (doorStatus == Status.open || doorStatus == Status.moving)
            {
                waitRoutine = StartCoroutine(WaitForClose());
            }
        }
    }

    IEnumerator OpenDoor()
    {
        float t = 0f;

        Vector3 _LCPos = leftCurPosition;
        Vector3 _RCPos = rightCurPosition;

        doorStatus = Status.moving;

        while (t < 1) 
        {
            t += Time.deltaTime * doorSpeed;
            leftDoor.localPosition = Vector3.Slerp(_LCPos, leftOpenPosition, t);
            rightDoor.localPosition = Vector3.Slerp(_RCPos, rightOpenPosition, t);

            leftCurPosition = leftDoor.localPosition;
            rightCurPosition = rightDoor.localPosition;

            yield return null;
        }

        doorStatus = Status.open;
    }

    Coroutine closeRoutine;
    IEnumerator CloseDoor()
    {

        doorStatus = Status.moving;
        float t = 0f;
        while(t < 1)
        { 
            t += Time.deltaTime * doorSpeed;
            leftDoor.localPosition = Vector3.Slerp(leftOpenPosition, leftClosePosition, t);
            rightDoor.localPosition = Vector3.Slerp(rightOpenPosition, rightClosePosition, t);

            leftCurPosition = leftDoor.localPosition;
            rightCurPosition = rightDoor.localPosition;

            yield return null;
        }

        doorStatus = Status.close;
    }

    Coroutine waitRoutine;
    IEnumerator WaitForClose()
    {
        yield return new WaitForSeconds(waitCount);
        closeRoutine = StartCoroutine(CloseDoor());
    }
}
