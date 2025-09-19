using UnityEngine;
using UnityEngine.UI;

public class SquareRawImage : MonoBehaviour
{
    public RawImage rawImage;           // ��������� RawImage, � �������� �������� ���������
    public RectTransform canvasRect;    // Canvas ��� ������������ ������, ������������ ������ ������
    public int BordersPercent;

    void Update()
    {
        AdjustRawImageToSquare();
    }

    void OnValidate()
    {
        AdjustRawImageToSquare();
    }

    void OnCanvasChange()
    {
        AdjustRawImageToSquare();
    }

    void AdjustRawImageToSquare()
    {
        // �������� ����������� ������ ������ (������ ��� ������)
        float maxDimension = Mathf.Max(canvasRect.rect.width, canvasRect.rect.height);
        float minDimension = Mathf.Min(canvasRect.rect.width, canvasRect.rect.height);

        maxDimension -= (maxDimension / 100) * BordersPercent;
        minDimension -= (minDimension / 100) * BordersPercent;

        // �������� ���������� ������� RawImage
        rawImage.rectTransform.sizeDelta = new Vector2(maxDimension, minDimension);
    }
}