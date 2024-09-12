using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject spaceShip;
    public GameObject player;
    public float moveSpeed = 0.025f;

    public Slider progressBar;

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
        //playerAnim.SetTrigger("IsStart");
        //StartCoroutine(RotateCharacter());
        StartCoroutine(LoadSceneProgress("MainScene"));
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

    IEnumerator LoadSceneProgress(string _sceneName)
    {
        progressBar.value = 0f;

        AsyncOperation loading = SceneManager.LoadSceneAsync(_sceneName);
        loading.allowSceneActivation = false; // 로드 완료되어도 0.9에서 기다림
        float timer = 4f;

        while (!loading.isDone) // 로딩이 끝나기 전까지
        {
            yield return null;
            timer -= Time.deltaTime;

            progressBar.value = loading.progress;
            if(loading.progress >= 0.9f)
            {
                loading.allowSceneActivation = true;
            }
        }

        yield return new WaitForSeconds(timer);
    }
}
