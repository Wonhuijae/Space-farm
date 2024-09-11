using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;

    private Rigidbody playerRB;
    private AudioSource playerAS;
    private PlayerInput playerInput;
    private Animator playerAnim;
    private Camera playerCam;

    public int jumpCount;
    public bool isGround;

    bool IKActive;
    public Transform handPos;
    public Transform toolPos;

    // Start is called before the first frame update
    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAS = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();

        jumpCount = 1;
        isGround = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerInput.hValue != 0 || playerInput.vValue != 0)
        {
            Move();
            Rotate();
        }
        
        Jump();
        float move = playerInput.vValue; //playerInput.hValue != 0 ? playerInput.hValue : playerInput.vValue;

        playerAnim.SetFloat("Move", move);
    }

    // 이동 위치 = 현재 위치 + 방향 * 시간 * 속도
    void Move()
    {
        // 캐릭터 기준
        Vector3 moveDirection = playerInput.vValue * transform.forward;

        // 이동
        playerRB.MovePosition(playerRB.position + moveDirection * Time.deltaTime * moveSpeed);
        /*
        // 카메라 기준
        // 이동 방향
        //Vector3 camRight = Camera.main.transform.right;
        //Vector3 camForward = Camera.main.transform.forward;

        //camRight.y = 0;
        //camForward.y = 0;

        //Vector3 moveDirection = (playerInput.hValue * camRight + playerInput.vValue * camForward).normalized;

        //if (moveDirection != Vector3.zero)
        //{
            
        //    // 이동 방향으로 캐릭터 회전
        //    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        //    playerRB.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        //}
        //playerRB.MovePosition(playerRB.position + moveDirection * Time.deltaTime * moveSpeed);
        */
    }

    void Rotate()
    {
        float turn = playerInput.hValue * rotateSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, turn, 0f);
        playerRB.rotation *= targetRotation; 
    }

    void Jump()
    {
        if (isGround && jumpCount > 0 && playerInput.isJump) 
        {
            playerRB.AddForce(Vector3.up * 10f, ForceMode.Impulse );
            jumpCount--;
            isGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
            isGround = true;
        }
    }

    private void OnAnimatorIK()
    {
        if(playerAnim)
        {
            if(IKActive)
            {
                if(toolPos != null)
                {
                    playerAnim.SetLookAtWeight(1);
                    playerAnim.SetLookAtPosition(toolPos.position);
                }

                if(handPos != null)
                {
                    playerAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    playerAnim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

                    playerAnim.SetIKPosition(AvatarIKGoal.RightHand, handPos.position);
                    playerAnim.SetIKRotation(AvatarIKGoal.RightHand, handPos.rotation);
                }
            }
        }
    }
}
