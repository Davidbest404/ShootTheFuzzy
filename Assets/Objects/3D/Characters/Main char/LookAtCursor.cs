using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    // Компонент персонажа (Rigidbody или Transform)
    public CharacterController characterController;

    void Update()
    {
        if (!characterController)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        // Проверяем наличие препятствия перед камерой
        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 targetPosition = hitInfo.point;

            // Определяем направление от игрока к точке удара луча
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f; // Игнорируем высоту

            Quaternion newRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8); // Для плавного поворота
        }
    }
}