using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    // ������, ��������� ������ ����
    public Camera CameraMain;

    // ������������ ������ ���� ���������
    public Transform CharacterBody;

    // ������ ��������� (��� ������������� ��������)
    public Transform Head;

    // �������� ��������������� ��������
    public float hRotationSpeed = 10f;

    // �������� �������� ������
    public float headRotationSpeed = 8f;

    // ����������� ���� ������� ������
    public float minAngle = -60f; // ������ �����
    public float maxAngle = 60f;  // ������ ����

    // ������� ����� ��� ��������
    [SerializeField]
    private LayerMask layerMask;

    void Update()
    {
        HorizontalRotation();   // �������������� �������� ����
        VerticalRotation();     // ������������ �������� ������
    }

    /// ����������� �������������� ������� ���� ��������� � ����� ��������� ����
    void HorizontalRotation()
    {
        Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            // ����������� ������� �� �����������
            Vector3 aimDirection = hitInfo.point - CharacterBody.position;
            aimDirection.y = 0; // ������� ������������ ����������
            aimDirection.Normalize();

            // ������� ����������
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, Vector3.up);

            // ������������ ���� ���������
            CharacterBody.rotation = Quaternion.RotateTowards(
                CharacterBody.rotation,
                targetRotation,
                hRotationSpeed * Time.deltaTime
            );
        }
    }

    /// ������������ ������ ������������ �������� ������ ���������
    void VerticalRotation()
    {
        Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            // ������ ����������� ������� �� ������ � ����� ���������
            Vector3 lookDir = hitInfo.point - Head.position;
            lookDir.Normalize();

            // ������������ ���� ������� ������ ������������ �����������
            float angle = Mathf.Atan2(lookDir.y, lookDir.magnitude) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, minAngle, maxAngle); // ������������ ����

            // ����������� ���� � ���������� ����������
            Quaternion targetRot = Quaternion.Euler(-angle, 0, 0);

            // ������������ ������ �� ������ ��� (�������� X)
            Head.localRotation = Quaternion.RotateTowards(
                Head.localRotation,
                targetRot,
                headRotationSpeed * Time.deltaTime
            );
        }
    }

    /// ����� ��� ������������ ����������� � ���������
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