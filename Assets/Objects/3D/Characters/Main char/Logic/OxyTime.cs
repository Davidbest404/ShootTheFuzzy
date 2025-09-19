using UnityEngine;

public class OxyTimer : MonoBehaviour
{
    public Light targetLight;               // Целевое освещение
    public Color startColor = Color.green;  // Начальный цвет
    public Color endColor = Color.red;      // Конечный цвет
    public float transitionDuration = 5f;   // Длительность перехода

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
        targetLight.color = startColor; // Вернем цвет обратно
    }
}