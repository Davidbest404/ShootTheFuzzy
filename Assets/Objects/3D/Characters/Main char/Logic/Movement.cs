using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Физическое тело персонажа
    private Rigidbody rb;

    // Действия ввода
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction sprintAction;

    // Скорость обычного хода и спринта
    public float walkSpeed = 5.0f;
    public float sprintMultiplier = 1.5f;

    void Awake()
    {
        // Получаем физическое тело
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on the character!");
        }

        // Получаем нужные действия
        moveAction = inputActions.FindActionMap("Player").FindAction("Move");
        sprintAction = inputActions.FindActionMap("Player").FindAction("Sprint");
    }

    void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.Enable();
        }
        if (sprintAction != null)
        {
            sprintAction.Enable();
        }
    }

    void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.Disable();
        }
        if (sprintAction != null)
        {
            sprintAction.Disable();
        }
    }

    void FixedUpdate()
    {
        // Чтение сигналов движения
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // Определяем финальную скорость
        float finalSpeed = walkSpeed * (sprintAction.triggered ? sprintMultiplier : 1.0f);

        // Формирование вектора движения
        Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y);
        moveVector = transform.TransformDirection(moveVector.normalized * finalSpeed);

        // Применение скорости
        rb.linearVelocity = new Vector3(moveVector.x, rb.linearVelocity.y, moveVector.z);
    }
}