using UnityEngine;

public class HelmetBeamController : MonoBehaviour
{
    // ����� ������� (���)
    public LineRenderer beamRenderer;

    // ������������ ��������� ����
    public float maxDistance = 100f;

    // ������ ��������� (�������� ����� ����)
    public Transform startPoint;

    void LateUpdate()
    {
        // ��������� ���
        Ray ray = new Ray(startPoint.position, transform.forward);
        RaycastHit hitInfo;

        bool didHit = Physics.Raycast(ray, out hitInfo, maxDistance);

        // ������ ��� �� ���������� �������
        beamRenderer.SetPosition(0, startPoint.position);
        beamRenderer.SetPosition(1, didHit ? hitInfo.point : (startPoint.position + transform.forward * maxDistance));
    }
}