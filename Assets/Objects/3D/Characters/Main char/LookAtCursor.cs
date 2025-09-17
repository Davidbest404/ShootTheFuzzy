using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    // ��������� ��������� (Rigidbody ��� Transform)
    public CharacterController characterController;

    void Update()
    {
        if (!characterController)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        // ��������� ������� ����������� ����� �������
        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 targetPosition = hitInfo.point;

            // ���������� ����������� �� ������ � ����� ����� ����
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f; // ���������� ������

            Quaternion newRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8); // ��� �������� ��������
        }
    }
}