using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject spaceShip;
    public GameObject player;
    public float moveSpeed = 0.025f;

    Rigidbody playerRB;
    Animator playerAnim;
    Vector3 targetDirection;
    
    float t = 0f;
    Vector3 startPos;
    Vector3 targetPos;
    private void Awake()
    {
        targetDirection = - player.transform.position + spaceShip.transform.position;
        playerRB = player.GetComponent<Rigidbody>();
        playerAnim = player.GetComponent<Animator>();

        startPos = player.transform.position;
        targetPos =spaceShip.transform.position;
    }

    public void GameStart()
    {
        playerAnim.SetTrigger("IsStart");
        StartCoroutine(RotateCharacter());
    }

    IEnumerator RotateCharacter()
    {
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);

        while (Quaternion.Angle(player.transform.rotation, lookRotation) > 0.1f)
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, lookRotation, 0.025f);
            yield return null;
        }

       StartCoroutine(MoveCharacter());
    }

    IEnumerator MoveCharacter()
    {
        while(t < 1f)
        {
            t += (Time.deltaTime * moveSpeed);

            player.transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }
    }
}
