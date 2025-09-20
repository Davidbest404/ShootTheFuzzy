using UnityEngine;

public class SmoothColorTransition : MonoBehaviour
{
    public Light targetLight;       // Цель света, цвет которого меняется
    public Color startColor;        // Начальный цвет
    public Color endColor;          // Конечный цвет
    public float transitionTime = 5f; // Продолжительность изменения цвета (таймер)
    public float currentTime = 0f;
    public bool isTransitioning = true;

    void Update()
    {
        if (isTransitioning)
        {
            // Используем нормализованный таймер для плавного перехода цветов
            float normalizedTime = GetNormalizedTimerValue(transitionTime);

            // Интерполируем цвет между начальной и конечной точкой
            float t = (float)i / steps; // нормализуем значение текущего шага в диапазоне от 0 до 1

            // интерполируем каждый компонент цвета отдельно
            float r = Mathf.Lerp(startColor.r, endColor.r, t);
            float g = Mathf.Lerp(startColor.g, endColor.g, t);
            float b = Mathf.Lerp(startColor.b, endColor.b, t);

            lightSource.color = new Color(r, g, b);

            // Обновляем текущее время
            currentTime += Time.deltaTime;

            // Если переход завершён, останавливаемся
            if (currentTime >= transitionTime)
            {
                FinishTransition();
            }
        }
    }

    /// Возвращает нормализованную величину прохождения времени от 0.01 до 1.00
    public float GetNormalizedTimerValue(float maxDuration)
    {
        // Вычисляем пропорцию прошедшего времени
        float elapsedTime = Mathf.Min(currentTime, maxDuration);

        // Преобразовываем её в нужный нам диапазон от 0.01 до 1.00
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
        targetLight.color = endColor; // Устанавливаем итоговый цвет
    }
}