using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealisticTurretController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform basePart;
    public Transform barrelPart;
    public Transform gunExitPoint;
    public float shootForce = 500f;
    public float hRotationSpeed = 10f;
    public float vRotationSpeed = 5f;
    public float minAngle = -45f;
    public float maxAngle = 45f;

    private float currentVerticalAngle = 0f;

    void Update()
    {
        HorizontalRotation();
        VerticalRotation();
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void HorizontalRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 aimDirection = hitInfo.point - basePart.position;
            aimDirection.y = 0;
            aimDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
            basePart.rotation = Quaternion.Lerp(basePart.rotation, targetRotation, Time.deltaTime * hRotationSpeed);
        }
    }

    void VerticalRotation()
    {
        float verticalDelta = Input.GetAxis("Mouse Y");
        currentVerticalAngle += verticalDelta * vRotationSpeed;
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minAngle, maxAngle);

        barrelPart.localEulerAngles = new Vector3(-currentVerticalAngle, barrelPart.localEulerAngles.y, 0);
    }

    void Fire()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, gunExitPoint.position, Quaternion.identity);
        projectileInstance.GetComponent<Rigidbody>().AddForce(barrelPart.forward * shootForce, ForceMode.Impulse);
    }

    void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, hitInfo.point);
        }
    }
}