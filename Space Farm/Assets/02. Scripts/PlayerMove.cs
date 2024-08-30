using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    private Rigidbody playerRB;
    private AudioSource playerAS;
    private PlayerInput playerInput;
    private Animator playerAnim;
    private Camera playerCam;

    public int jumpCount;
    public bool isGround;

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
        Move();
        Rotate();
        Jump();
        CameraRotate();
    }

    // 이동 위치 = 현재 위치 + 방향 * 시간 * 속도
    void Move()
    {
        // 이동 방향
        Vector3 moveDirection = new Vector3(playerInput.hValue, 0, playerInput.vValue).normalized;

        playerRB.MovePosition(playerRB.position + moveDirection * Time.deltaTime * moveSpeed);
    }

    void Rotate()
    {

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

    void CameraRotate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
            isGround = true;
        }
    }
}
