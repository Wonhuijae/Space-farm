using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public GameObject panleColorPic;
    public GameObject[] BTNs;

    public Image palette;
    public Image cursor;

    public Color selectedColor;

    private Vector2 paletteSize;
    private CircleCollider2D paletteColl;
    private PlayerManager playerInstance;

    private void Awake()
    {
        playerInstance = PlayerManager.instance;

        paletteColl = GetComponent<CircleCollider2D>();

        paletteSize = new Vector2(palette.GetComponent<RectTransform>().rect.width,
                                  palette.GetComponent<RectTransform>().rect.height);
    }

    private void SelectColor()
    {
        Vector3 offset = Input.mousePosition - transform.position;
        Vector3 diff = Vector3.ClampMagnitude(offset, paletteColl.radius); // 객체 이동 제한 메서드

        cursor.transform.position = transform.position + diff;

        selectedColor = GetColor();
        cursor.color = selectedColor;
    }

    public void PointerDown()
    {
        SelectColor();
    }

    public void PointerDrag()
    {
        SelectColor();
    }

    Color GetColor()
    {
        Vector2 palettePos = palette.transform.position;
        Vector2 cursorPos = cursor.transform.position;

        Vector2 pos = cursorPos - palettePos + paletteSize * 0.5f;
        Vector2 normalPos = new Vector2(pos.x / palette.GetComponent<RectTransform>().rect.width,
                                        pos.y / palette.GetComponent<RectTransform>().rect.height);

        Texture2D t = palette.mainTexture as Texture2D;
        Color c = t.GetPixelBilinear(normalPos.x, normalPos.y);

        return c;
    }

    public void Cancle()
    {
        panleColorPic.SetActive(false);
        foreach(var o in BTNs)
        {
            o.SetActive(true);
        }
    }

    public void SaveColor()
    {
        playerInstance.SetColor(selectedColor);
    }
}
