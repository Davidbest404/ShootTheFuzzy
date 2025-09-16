using UnityEngine;
using UnityEngine.UI;

public class SquareRawImage : MonoBehaviour
{
    public RawImage rawImage;           // Компонент RawImage, к которому применим изменения
    public RectTransform canvasRect;    // Canvas или родительский объект, определяющий размер экрана
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
        // Получаем минимальный размер экрана (ширину или высоту)
        float minDimension = Mathf.Min(canvasRect.rect.width, canvasRect.rect.height);

        minDimension -= (minDimension / 100) * BordersPercent;

        // Задаваем квадратные размеры RawImage
        rawImage.rectTransform.sizeDelta = new Vector2(minDimension, minDimension);
    }
}