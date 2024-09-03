using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSystemInput : MonoBehaviour
{
    // ���� ī�޶�
    public Camera cam;
    public Vector3 mousePos
    {
        get
        {
            _mousePos = Input.mousePosition; // ���� ���콺 ��ġ
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
    private Vector3 _mousePos; //  �ӽ� ����

    public Vector3Int cellPos
    {
        get
        {
            _cellPos = grid.WorldToCell(mousePos);

            return _cellPos;
        }
    }
    private Vector3Int _cellPos;
    public LayerMask planeLayer; // ���콺 ������ ���� ���̾�
    public GameObject previewObj; // �� �̸����� ������Ʈ

    private Grid grid;

    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();

        if (grid == null) Debug.Log("�׸��� ����");
    }

    private void Update()
    {
        //previewObj.transform.position = mousePos;
        previewObj.transform.position = grid.CellToWorld(cellPos);
    }
}
