using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum ToolState
{
    None,
    hoe,
    trowel,
    watercan,
    sickle,
    traktor,
    sprinkler
 }

public enum SeedState
{
    None,
    Apple,
    Tomato,
    Wheat
}

public enum Tier
{
    Basic,
    Intermediate,
    Advanced
}

public class FarmSystem : MonoBehaviour
{
    public static FarmSystem instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<FarmSystem>();
            }

            return _instance;
        }
    }
    private static FarmSystem _instance;
    public GameManager gmInstace;

    // 메인 카메라
    public Camera cam;
    public Vector3 mousePos
    {
        get
        {
            _mousePos = Input.mousePosition;
            _mousePos.z = cam.nearClipPlane;
            Ray ray = cam.ScreenPointToRay(_mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100000, planeLayer))
            {
                _mousePos = hit.point;
            }
            return _mousePos;
        }
    }
    private Vector3 _mousePos;

    public Vector3Int cellPos
    {
        get
        {
            _cellPos = grid.WorldToCell(mousePos);


            return _cellPos;
        }
    }
    private Vector3Int _cellPos;
    public LayerMask planeLayer;
    public GameObject previewObj;
    public GameObject originalField;
    public GameObject SeedPopup;

    private Grid grid;
    private UIManager UIinstance;
    private bool isOverLapped;
    

    private Dictionary<SeedState, SeedData> seedsDict = new();

    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        UIinstance = FindObjectOfType<UIManager>();
        isOverLapped = false;

        gmInstace = GameManager.Instance;
        if (gmInstace != null)
        {
            foreach(SeedData s in gmInstace.GetSeedData())
            {
                seedsDict.Add(s.seedState, s);
            }
        }
    }

    private void Update()
    {
        if (gmInstace.toolState == ToolState.hoe)
        {
            previewObj.SetActive(true);
            previewObj.transform.position = grid.CellToWorld(cellPos);
            if (!isOverLapped)
            {
                if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 fieldPos = grid.CellToWorld(cellPos);
                    Instantiate(originalField, fieldPos, Quaternion.identity);
                }
            }
            else
            {
               
            }
        }
        else
        {
            previewObj.SetActive(false);
        }
    }

    public void ChangeStateCollEnter() // 충돌 상태가 된다
    {
        isOverLapped = true;
    }

    public void ChangeStateCollExit() // 충돌 상태가 아니게 된다
    {
        isOverLapped = false;
    }

    public void SetSeed(SeedState _s)
    {
        gmInstace.ChangeSeed(_s);
    }

    public SeedData GetDict(SeedState _s)
    {
        return seedsDict[_s];
    }
}
