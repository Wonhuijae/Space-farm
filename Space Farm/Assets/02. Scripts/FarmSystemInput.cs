using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSystemInput : MonoBehaviour
{
    public static FarmSystemInput instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<FarmSystemInput>();
            }

            return _instance;
        }
    }

    private static FarmSystemInput _instance;

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

    private Grid grid;
    private UIManager UIinstance;
    private bool isOverLapped;

    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        UIinstance = FindObjectOfType<UIManager>();
        isOverLapped = false;
        previewObj.SetActive(false);
    }

    private void Update()
    {
        previewObj.transform.position = grid.CellToWorld(cellPos);

        if (UIinstance.curActiveShortCut == 0 && Input.GetMouseButtonDown(0))
        {
            if (!isOverLapped)
            {
                Vector3 fieldPos = grid.CellToWorld(cellPos);
                Instantiate(originalField, fieldPos, Quaternion.identity);
                Debug.Log("설치 완료");
            }
            else
            {
               Debug.Log("설치 불가");
            }
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
}
