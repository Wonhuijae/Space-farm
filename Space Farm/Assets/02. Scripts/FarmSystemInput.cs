using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSystemInput : MonoBehaviour
{
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
    public GameObject originalFiled;

    private Grid grid;

    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();
    }

    private void Update()
    {
        previewObj.transform.position = grid.CellToWorld(cellPos);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 fieldPos = grid.CellToWorld(cellPos);
            Instantiate(originalFiled, fieldPos, Quaternion.identity);
        }
    }
}
