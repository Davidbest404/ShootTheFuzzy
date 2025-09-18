using UnityEngine;

public class DeathRagdoll : MonoBehaviour
{
    // ����� �������� ������ ������
    public Transform spawnPoint;

    // ������ ������ (����� �������������� ��� ����������� ���������)
    public GameObject playerPrefab;

    // ��� ���������� ��������� (�����)
    private Rigidbody[] rigidbodies;

    // �������� ��������� ���������
    private Collider mainCollider;

    // ������ ���������� � ragdoll-������
    private bool isInRagdollMode = false;

    void Awake()
    {
        // ���� ��� ��������� (����� ���������)
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        // �������� �������� ���������
        mainCollider = GetComponent<Collider>();
    }

    // ������� ��� �������� ��������� � ����� ragdoll
    public void EnterRagdoll()
    {
        if (!isInRagdollMode)
        {
            Debug.Log("�������� ������� � ����� ragdoll");

            // ��������� ���������� ����� �������������� �� ����� ���������
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            // �������� ��������� ������� (����� ���������� ��� ��� �������� �����)
            mainCollider.enabled = true;

            // ������� ������ ������ � ��������� �����
            SpawnNewPlayer();

            // ������ ����� � ragdoll-������
            isInRagdollMode = true;
        }
    }

    // ���������� �������� ������ ������
    private void SpawnNewPlayer()
    {
        // ��������� ������ ������ � ��������� ����� ������
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // ��������, ��� �������� ������� ���������
        Debug.Log("������ ������ �������!");
    }
}