using UnityEngine;

public class Oxygen : MonoBehaviour
{
    // Время ожидания перед срабатыванием смерти (секунды)
    public float timeBeforeDeath = 10f;

    // Класс DeathRagdoll для вызова процедуры смерти
    private DeathRagdoll deathRagdoll;

    // Переменная для хранения оставшегося времени
    private float remainingTime;

    // Метка активности таймера
    private bool timerActive = false;

    void Start()
    {
        // Находим компонент DeathRagdoll
        deathRagdoll = GetComponent<DeathRagdoll>();

        // Начинаем таймер
        StartTimer();
    }

    // Метод для запуска таймера
    public void StartTimer()
    {
        remainingTime = timeBeforeDeath;
        timerActive = true;
    }

    // Логика обновления таймера
    void Update()
    {
        if (timerActive && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            // Если время вышло
            if (remainingTime <= 0)
            {
                timerActive = false;
                deathRagdoll.EnterRagdoll(); // Вызываем смерть персонажа
            }
        }
    }

    // Возможность принудительно остановить таймер
    public void StopTimer()
    {
        timerActive = false;
    }

    // Возврат текущего состояния таймера
    public bool IsTimerRunning()
    {
        return timerActive;
    }
}