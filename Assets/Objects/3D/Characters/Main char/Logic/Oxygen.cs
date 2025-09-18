using UnityEngine;

public class Oxygen : MonoBehaviour
{
    // ����� �������� ����� ������������� ������ (�������)
    public float timeBeforeDeath = 10f;

    // ����� DeathRagdoll ��� ������ ��������� ������
    private DeathRagdoll deathRagdoll;

    // ���������� ��� �������� ����������� �������
    private float remainingTime;

    // ����� ���������� �������
    private bool timerActive = false;

    void Start()
    {
        // ������� ��������� DeathRagdoll
        deathRagdoll = GetComponent<DeathRagdoll>();

        // �������� ������
        StartTimer();
    }

    // ����� ��� ������� �������
    public void StartTimer()
    {
        remainingTime = timeBeforeDeath;
        timerActive = true;
    }

    // ������ ���������� �������
    void Update()
    {
        if (timerActive && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            // ���� ����� �����
            if (remainingTime <= 0)
            {
                timerActive = false;
                deathRagdoll.EnterRagdoll(); // �������� ������ ���������
            }
        }
    }

    // ����������� ������������� ���������� ������
    public void StopTimer()
    {
        timerActive = false;
    }

    // ������� �������� ��������� �������
    public bool IsTimerRunning()
    {
        return timerActive;
    }
}