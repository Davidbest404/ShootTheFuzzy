using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    // Основная камера, смотрящая сверху вниз
    public Camera CameraMain;

    // Контейнер тела персонажа (родительский объект)
    public Transform CharacterBody;

    // Скорость горизонтального вращения
    public float hRotationSpeed = 10f;

    // Маска слоёв для фильтра (если нужно отбрасывать некоторые слои)
    [SerializeField]
    private LayerMask layerMask;

    void Update()
    {
        HorizontalRotation();
    }

    /// <summary>
    /// Осуществляет горизонтальный поворот персонажа к точке попадания луча
    /// </summary>
    void HorizontalRotation()
    {
        Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            // Определяем точку удара луча
            Vector3 aimDirection = hitInfo.point - CharacterBody.position;
            aimDirection.y = 0; // Только поворот по горизонтали
            aimDirection.Normalize();

            // Создаем целевую ориентацию
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, Vector3.up);

            // Плавно поворачиваем тело персонажа
            CharacterBody.rotation = Quaternion.RotateTowards(CharacterBody.rotation, targetRotation, hRotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Метод для отображения линий прицеливания в режиме редактирования
    /// </summary>
    void OnDrawGizmos()
    {
        Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(CameraMain.transform.position, hitInfo.point);
        }
    }
}