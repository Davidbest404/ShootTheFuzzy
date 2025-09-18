using UnityEngine;

public class HelmetBeamController : MonoBehaviour
{
    // Линия рендера (луч)
    public LineRenderer beamRenderer;

    // Максимальная дальность луча
    public float maxDistance = 100f;

    // Голова персонажа (исходная точка луча)
    public Transform startPoint;

    void LateUpdate()
    {
        // Формируем луч
        Ray ray = new Ray(startPoint.position, transform.forward);
        RaycastHit hitInfo;

        bool didHit = Physics.Raycast(ray, out hitInfo, maxDistance);

        // Начнем луч из указанного объекта
        beamRenderer.SetPosition(0, startPoint.position);
        beamRenderer.SetPosition(1, didHit ? hitInfo.point : (startPoint.position + transform.forward * maxDistance));
    }
}