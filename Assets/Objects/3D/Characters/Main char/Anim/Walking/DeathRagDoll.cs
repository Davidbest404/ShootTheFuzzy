using UnityEngine;

public class DeathRagdoll : MonoBehaviour
{
    // Место респауна нового игрока
    public Transform spawnPoint;

    // Префаб игрока (будет использоваться для воссоздания персонажа)
    public GameObject playerPrefab;

    // Все ригидбоуди персонажа (кости)
    private Rigidbody[] rigidbodies;

    // Основной коллайдер персонажа
    private Collider mainCollider;

    // Статус пребывания в ragdoll-режиме
    private bool isInRagdollMode = false;

    void Awake()
    {
        // Ищем все ригидбоды (кости персонажа)
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        // Получаем основной коллайдер
        mainCollider = GetComponent<Collider>();
    }

    // Функция для перевода персонажа в режим ragdoll
    public void EnterRagdoll()
    {
        if (!isInRagdollMode)
        {
            Debug.Log("Персонаж перешел в режим ragdoll");

            // Разрешаем физическим силам воздействовать на кости персонажа
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            // Включаем коллайдер обратно (после отключения его при создании героя)
            mainCollider.enabled = true;

            // Создаем нового игрока в выбранной точке
            SpawnNewPlayer();

            // Теперь герой в ragdoll-режиме
            isInRagdollMode = true;
        }
    }

    // Функционал создания нового игрока
    private void SpawnNewPlayer()
    {
        // Клонируем нового игрока в указанной точке спавна
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // Сообщаем, что персонаж успешно воссоздан
        Debug.Log("Нового игрока создали!");
    }
}