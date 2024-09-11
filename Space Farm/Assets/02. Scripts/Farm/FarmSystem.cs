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
    private GameManager gmInstace;
    private PlayerManager plInstace;
    public GameObject[] VFXs;

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

    public GameObject previewS;
    public GameObject OriginalS;

    private Grid grid;
    private UIManager UIinstance;
    private bool isOverLappedField;
    private bool isOverLappedSprinkler;

    private Dictionary<ToolState, ToolData> toolsDict = new();
    private Dictionary<SeedState, SeedData> seedsDict = new();

    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        UIinstance = FindObjectOfType<UIManager>();
        isOverLappedField = false;

        gmInstace = GameManager.Instance;
        plInstace = PlayerManager.instance;
        Debug.Log(plInstace == null);

        if (gmInstace != null)
        {
            foreach(SeedData s in gmInstace.GetSeedData())
            {
                seedsDict.Add(s.seedState, s);
            }

            foreach(ToolData t in gmInstace.GetToolData())
            {
                toolsDict.Add(t.toolState, t);
            }
        }
    }

    private void Update()
    {
        if (gmInstace.toolState == ToolState.None) return;

        if (gmInstace.toolState == ToolState.hoe)
        {
            previewObj.SetActive(true);
            previewObj.transform.position = grid.CellToWorld(cellPos);
        }
        else if(gmInstace.toolState ==ToolState.sprinkler)
        {
            previewS.SetActive(true);
            previewS.transform.position = grid.CellToWorld(cellPos);
        }
        else
        {
            previewObj.SetActive(false);
            previewS.SetActive(false);
        }

        ToolData d = toolsDict[gmInstace.toolState];
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 fieldPos = grid.CellToWorld(cellPos);

            switch (gmInstace.toolState)
            {
                case ToolState.hoe:
                    if (!isOverLappedField)
                    {
                        plInstace.GetAnim().SetTrigger(d.AnimTrigger);
                        Instantiate(originalField, fieldPos, Quaternion.identity);
                        Destroy(Instantiate(VFXs[0], new Vector3(fieldPos.x, fieldPos.y + 0.385f, fieldPos.z), Quaternion.identity), 5f);
                    }
                    break;
                case ToolState.sprinkler:
                    Debug.Log(isOverLappedSprinkler);
                    if (!isOverLappedSprinkler)
                    {
                       
                        plInstace.GetAnim().SetTrigger(d.AnimTrigger);
                        Instantiate(OriginalS, transform.position, Quaternion.identity);
                    }
                    break;
            }
        }
    }


    public void ChangeStateCollEnter() // 충돌 상태가 된다
    {
        isOverLappedField = true;
    }

    public void ChangeStateCollExit() // 충돌 상태가 아니게 된다
    {
        isOverLappedField = false;
    }
    
    public void ChangeStateCollEnterSprin() // 충돌 상태가 된다
    {
        isOverLappedSprinkler = true;
    }

    public void ChangeStateCollExitSprin() // 충돌 상태가 아니게 된다
    {
        isOverLappedSprinkler = false;
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
