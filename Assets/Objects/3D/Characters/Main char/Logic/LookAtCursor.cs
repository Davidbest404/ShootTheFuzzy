using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    // Камера, смотрящая сверху вниз
    public Camera CameraMain;

    // Родительский объект тела персонажа
    public Transform CharacterBody;

    // Голову персонажа (для вертикального вращения)
    public Transform Head;

    // Скорость горизонтального вращения
    public float hRotationSpeed = 10f;

    // Скорость вращения головы
    public float headRotationSpeed = 8f;

    // Ограничения угла наклона головы
    public float minAngle = -60f; // Наклон вверх
    public float maxAngle = 60f;  // Наклон вниз

    // Масштаб слоев для фильтров
    [SerializeField]
    private LayerMask layerMask;

    void Update()
    {
        HorizontalRotation();   // Горизонтальное вращение тела
        VerticalRotation();     // Вертикальное вращение головы
    }

    /// Осущестляет горизонтальный поворот тела персонажа к точке попадания луча
    void HorizontalRotation()
    {
        Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            // Направление взгляда по горизонтали
            Vector3 aimDirection = hitInfo.point - CharacterBody.position;
            aimDirection.y = 0; // Очищаем вертикальную компоненту
            aimDirection.Normalize();

            // Цельная ориентация
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, Vector3.up);

            // Поворачиваем тело персонажа
            CharacterBody.rotation = Quaternion.RotateTowards(
                CharacterBody.rotation,
                targetRotation,
                hRotationSpeed * Time.deltaTime
            );
        }
    }

    /// Осуществляет точное вертикальное вращение головы персонажа
    void VerticalRotation()
    {
        Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            // Вектор направления взгляда от головы к точке попадания
            Vector3 lookDir = hitInfo.point - Head.position;
            lookDir.Normalize();

            // Рассчитываем угол наклона головы относительно горизонтали
            float angle = Mathf.Atan2(lookDir.y, lookDir.magnitude) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, minAngle, maxAngle); // Ограничиваем угол

            // Преобразуем угол в правильную ориентацию
            Quaternion targetRot = Quaternion.Euler(-angle, 0, 0);

            // Поворачиваем голову по нужной оси (локально X)
            Head.localRotation = Quaternion.RotateTowards(
                Head.localRotation,
                targetRot,
                headRotationSpeed * Time.deltaTime
            );
        }
    }

    /// Метод для визуализации направлений в редакторе
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