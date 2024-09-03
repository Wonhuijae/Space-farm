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
            _mousePos = Input.mousePosition; // 현재 마우스 위치
            _mousePos.z = cam.nearClipPlane;
            Ray ray = cam.ScreenPointToRay(_mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, planeLayer))
            {
                _mousePos = hit.point;
            }
            return _mousePos;
        }
    }
    private Vector3 _mousePos; //  임시 변수

    public Vector3Int cellPos
    {
        get
        {
            _cellPos = grid.WorldToCell(mousePos);

            return _cellPos;
        }
    }
    private Vector3Int _cellPos;
    public LayerMask planeLayer; // 마우스 감지에 사용될 레이어
    public GameObject previewObj; // 셀 미리보기 오브젝트

    private Grid grid;

    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();

        if (grid == null) Debug.Log("그리드 없음");
    }

    private void Update()
    {
        //previewObj.transform.position = mousePos;
        previewObj.transform.position = grid.CellToWorld(cellPos);
    }
}
