using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Movement Speed")]
    [SerializeField] private float walkSpeed = 2.5f;      // Скорость при патрулировании / дальнем расстоянии
    [SerializeField] private float chaseSpeed = 4.0f;      // Скорость при преследовании

    [Header("Chasing Settings")]
    [SerializeField] private float chaseDistance = 10f;    // На каком расстоянии начинать преследование
    [SerializeField] private float stopDistance = 0.5f;      // На каком расстоянии остановиться от игрока

    [Header("Optimization")]
    [SerializeField] private float updateInterval = 0.2f;  // Как часто обновлять цель (в секундах)

    [Header("References")]
    [SerializeField] private NavMeshAgent navMeshAgent;

    private float lastUpdateTime;

    private void Start()
    {
        // Автоматически получить NavMeshAgent, если не задан
        if (navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();

        // Проверка на ошибки
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent is missing on " + gameObject.name, this);
            enabled = false;
            return;
        }

        // Настроить начальные параметры агента
        navMeshAgent.updateRotation = true;
        navMeshAgent.updateUpAxis = true;
        navMeshAgent.stoppingDistance = stopDistance;
        navMeshAgent.speed = walkSpeed; // начальная скорость
    }

    private void Update()
    {
        if (player == null)
            return;

        // Оптимизация: не обновлять каждый кадр
        if (Time.time - lastUpdateTime < updateInterval)
            return;

        lastUpdateTime = Time.time;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            // Режим преследования
            navMeshAgent.speed = chaseSpeed;

            // Смещаем цель вокруг игрока, чтобы не толпиться в одной точке
            Vector3 directionFromPlayer = (transform.position - player.position).normalized;
            directionFromPlayer.y = 0f; // игнорируем высоту

            // Если моб слишком близко — направляем его немного в сторону от игрока
            if (directionFromPlayer.magnitude > 0.1f)
            {
                Vector3 targetPosition = player.position + directionFromPlayer * stopDistance;
                SetDestinationSafely(targetPosition);
            }
            else
            {
                // Если моб почти в центре — даём случайное смещение
                Vector2 randomCircle = Random.insideUnitCircle;
                Vector3 randomOffset = new Vector3(randomCircle.x, 0f, randomCircle.y) * stopDistance;
                SetDestinationSafely(player.position + randomOffset);
            }
        }
        else
        {
            // Вне зоны преследования — можно остановиться или патрулировать
            navMeshAgent.speed = walkSpeed;
            navMeshAgent.isStopped = true; // или реализуй патрулирование
        }
    }

    // Вспомогательный метод: безопасно устанавливает цель с проверкой NavMesh
    private void SetDestinationSafely(Vector3 target)
    {
        if (NavMesh.SamplePosition(target, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
            navMeshAgent.isStopped = false;
        }
        else
        {
            // Если не нашли ближайшую проходимую точку — идём к цели напрямую (может быть неидеально)
            navMeshAgent.SetDestination(target);
            navMeshAgent.isStopped = false;
        }
    }
}