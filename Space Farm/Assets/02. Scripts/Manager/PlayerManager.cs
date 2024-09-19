using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PlayerManager>();
            }

            return m_instance;
        }
    }
    private static PlayerManager m_instance;

    public PlayerData playerData;
    private PlayerInput playerInput;
    public GameObject CameraPos;
    public Transform ridePos;
    public Transform normalPos;

    AudioSource playerAs;
    public SkinnedMeshRenderer playerRD;
    GameManager gmInstance;
    Animator plAnim;

    public GameObject[] tools;
    public ParticleSystem Watercan;

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);    
        }

        playerAs = GetComponent<AudioSource>();

        playerRD.materials[1].color= playerData.color;
        plAnim = GetComponent<Animator>();

        gmInstance = GameManager.Instance;
        gmInstance.onToolsOn += ToolsOn;
        gmInstance.onAllToolsOff += AllToolsOff;

        playerInput = GetComponent<PlayerInput>();
    }

    public Animator GetAnim()
    {
        return plAnim;
    }

    public void ToolsOn(int idx)
    {
        AllToolsOff();
        if (idx == 3) return;
        tools[idx].SetActive(true);

        if (idx == 4)
        {
            playerRD.enabled = false;
            CameraPos.transform.position = ridePos.position;
        }
    }

    public void AllToolsOff()
    {
        foreach(var o in tools)
        {
            o.SetActive(false);
            playerRD.enabled = true;
            CameraPos.transform.position = normalPos.position;
        }
    }

    public void SetColor(Color _color)
    {
        playerRD.materials[1].color = _color;
        playerData.color = _color;
        gmInstance.SetColor(_color);
    }

    public bool IsPointerOverUIObject()
    {
        return playerInput.IsPoinerOverUIObject();
    }
}
