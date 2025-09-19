using UnityEngine;

public class OxyTimer : MonoBehaviour
{
    public Light targetLight;               // ������� ���������
    public Color startColor = Color.green;  // ��������� ����
    public Color endColor = Color.red;      // �������� ����
    public float transitionDuration = 5f;   // ������������ ��������

    private bool isTransitioning = false;
    private float elapsedTime = 0f;
    public void Update()
    {
        Debug.Log("Update called");

        if (targetLight != null && isTransitioning)
        {
            float normalizedTime = elapsedTime / transitionDuration;
            targetLight.color = Color.Lerp(startColor, endColor, normalizedTime);

            if (normalizedTime >= 1f)
            {
                FinishTransition();
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }
        }
    }

    public void StartTransition()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            elapsedTime = 0f;
        }
    }

    private void FinishTransition()
    {
        isTransitioning = false;
        elapsedTime = 0f;
        targetLight.color = startColor; // ������ ���� �������
    }
}