using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    public GameObject spaceShip;
    public GameObject player;
    public float moveSpeed = 0.025f;
    public AudioClip fireClip;

    public GameObject loadingPanel;
    public Slider progressBar;

    Animator playerAnim;
    AudioSource audioSource;
    Vector3 targetDirection;

    float t = 0f;
    Vector3 targetPos;
    private void Awake()
    {
        targetDirection = -player.transform.position + spaceShip.transform.position;
        playerAnim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        targetPos = spaceShip.transform.position;
    }

    public void GameStart()
    {
        playerAnim.SetTrigger("IsStart");
        audioSource.Stop();

        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        Vector3 targetRotation = lookRotation.eulerAngles;

        player.transform.DORotate(targetRotation, 1f);
        Invoke("Move", 0.8f);
    }

    void Move()
    {
        player.transform.DOMove(targetPos, 3.5f);
        Invoke("Ride", 3.8f);
    }

    void Ride()
    {
        playerAnim.SetTrigger("IsStop");
        Vector3 ridePos = player.transform.position;
        ridePos.y += 3;
        player.transform.DOMove(ridePos, 2f);

        Invoke("Depart", 2.5f);
    }

    void Depart()
    {
        player.SetActive(false);
        audioSource.PlayOneShot(fireClip);
        foreach (ParticleSystem p in spaceShip.GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
        Vector3 departPos = spaceShip.transform.position;
        departPos.y += 23;
        spaceShip.transform.DOMove(departPos, 2f);
        
        StartCoroutine(LoadSceneProgress("MainScene"));
    }

    IEnumerator LoadSceneProgress(string _sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        spaceShip.SetActive(false);
        loadingPanel.SetActive(true);
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
