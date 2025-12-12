using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Здоровье персонажа, доступно для изменения в инспекторе
    public int health = 100;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем попадание объекта с тегом "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Уменьшаем здоровье на 1 пункт
            health--;

            // Проверяем, достигло ли здоровье нуля
            if (health <= 0)
                Die();
        }
    }

    private void Die()
    {
        // Здесь добавляем нужный вам код уничтожения объекта
        Debug.Log("Объект умер!");
        Destroy(this.gameObject); // Например, уничтожить объект
    }
}