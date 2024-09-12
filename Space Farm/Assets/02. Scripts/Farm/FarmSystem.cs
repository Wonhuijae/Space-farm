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

    ToolData d;

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
        if (gmInstace.toolState == ToolState.hoe)
        {
            previewObj.SetActive(true);
            previewS.SetActive(false);
            previewObj.transform.position = grid.CellToWorld(cellPos);
            if (-5 <= cellPos.x && cellPos.x <= 5 &&
                        -10 <= cellPos.z && cellPos.z <= 11)
            {
                previewObj.GetComponent<OutlineShader>().OutlineColor = Color.green;
            }
            else
            {
                previewObj.GetComponent<OutlineShader>().OutlineColor = Color.red;
            }
        }
        else if(gmInstace.toolState == ToolState.sprinkler)
        {
            previewObj.SetActive(false);
            previewS.SetActive(true);
            previewS.transform.position = grid.CellToWorld(cellPos);

            if (-5 <= cellPos.x && cellPos.x <= 5 &&
                        -10 <= cellPos.z && cellPos.z <= 11)
            {
                previewS.GetComponent<OutlineShader>().OutlineColor = Color.green;
            }
            else
            {
                previewS.GetComponent<OutlineShader>().OutlineColor = Color.red;
            }
        }
        else
        {
            previewObj.SetActive(false);
            previewS.SetActive(false);
        }

        if (gmInstace.toolState != ToolState.None) d = toolsDict[gmInstace.toolState];

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        { 
       /* if (Input.touchCount > 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject() &&
                EventSystem.current.currentSelectedGameObject == null)
            {*/
                Vector3 fieldPos = grid.CellToWorld(cellPos);
                fieldPos.y = 0;
            Debug.Log(cellPos); // -5 x 5  -10 z 11

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
                    if (!isOverLappedSprinkler)
                    {
                        Vector3 newPos = new Vector3(fieldPos.x - 1.5f, fieldPos.y + 0.385f, fieldPos.z - 1f);

                        plInstace.GetAnim().SetTrigger(d.AnimTrigger);
                        Instantiate(OriginalS, newPos, Quaternion.identity);
                    }
                        break;
                }
            }
        //}
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
