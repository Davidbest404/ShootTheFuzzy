using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    // �������� ������, ��������� ������ ����
    public Camera CameraMain;

    // ��������� ���� ��������� (������������ ������)
    public Transform CharacterBody;

    // �������� ��������������� ��������
    public float hRotationSpeed = 10f;

    // ����� ���� ��� ������� (���� ����� ����������� ��������� ����)
    [SerializeField]
    private LayerMask layerMask;

    void Update()
    {
        HorizontalRotation();
    }

    /// <summary>
    /// ������������ �������������� ������� ��������� � ����� ��������� ����
    /// </summary>
    void HorizontalRotation()
    {
        Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            // ���������� ����� ����� ����
            Vector3 aimDirection = hitInfo.point - CharacterBody.position;
            aimDirection.y = 0; // ������ ������� �� �����������
            aimDirection.Normalize();

            // ������� ������� ����������
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, Vector3.up);

            // ������ ������������ ���� ���������
            CharacterBody.rotation = Quaternion.RotateTowards(CharacterBody.rotation, targetRotation, hRotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ����� ��� ����������� ����� ������������ � ������ ��������������
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