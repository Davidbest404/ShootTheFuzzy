using UnityEngine;

public class SmoothColorTransition : MonoBehaviour
{
    public Light targetLight;       // ���� �����, ���� �������� ��������
    public Color startColor;        // ��������� ����
    public Color endColor;          // �������� ����
    public float transitionTime = 5f; // ����������������� ��������� ����� (������)
    public float currentTime = 0f;
    public bool isTransitioning = true;

    void Update()
    {
        if (isTransitioning)
        {
            // ���������� ��������������� ������ ��� �������� �������� ������
            float normalizedTime = GetNormalizedTimerValue(transitionTime);

            // ������������� ���� ����� ��������� � �������� ������
            float t = (float)i / steps; // ����������� �������� �������� ���� � ��������� �� 0 �� 1

            // ������������� ������ ��������� ����� ��������
            float r = Mathf.Lerp(startColor.r, endColor.r, t);
            float g = Mathf.Lerp(startColor.g, endColor.g, t);
            float b = Mathf.Lerp(startColor.b, endColor.b, t);

            lightSource.color = new Color(r, g, b);

            // ��������� ������� �����
            currentTime += Time.deltaTime;

            // ���� ������� ��������, ���������������
            if (currentTime >= transitionTime)
            {
                FinishTransition();
            }
        }
    }

    /// ���������� ��������������� �������� ����������� ������� �� 0.01 �� 1.00
    public float GetNormalizedTimerValue(float maxDuration)
    {
        // ��������� ��������� ���������� �������
        float elapsedTime = Mathf.Min(currentTime, maxDuration);

        // ��������������� � � ������ ��� �������� �� 0.01 �� 1.00
        return Mathf.Max((elapsedTime / maxDuration) * 0.99f + 0.01f, 0.01f);
    }

    public void StartTransition()
    {
        isTransitioning = true;
        currentTime = 0f;
    }

    private void FinishTransition()
    {
        isTransitioning = false;
        currentTime = 0f;
        targetLight.color = endColor; // ������������� �������� ����
    }
}