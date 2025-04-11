using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f; // Умножение скорости при беге
    public LayerMask groundLayer; // Слой для клика

    [Header("Stamina Settings")]
    public float maxStamina = 5f;
    public float staminaRecoveryRate = 1f; // скорость восстановления в секунду
    public float staminaDrainRate = 1.5f; // скорость траты в секунду при беге

    private float currentStamina;
    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isSprinting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
        currentStamina = maxStamina;
    }

    void Update()
    {
        HandleMouseClick();
        HandleSprintInput();
        RegenerateStamina();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    /// Обрабатывает клик мыши и устанавливает новую точку назначения

    void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
            {
                targetPosition = hit.point;
                isMoving = true;
            }
        }
    }

    /// Обрабатывает нажатие Shift для ускорения и учитывает выносливость
    void HandleSprintInput()
    {
        // Проверка: ПКМ нажата, есть выносливость и игрок в движении
        if (Input.GetMouseButton(1) && currentStamina > 0f && isMoving)
        {
            isSprinting = true;
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else
        {
            isSprinting = false;
        }
    }

    /// Восстанавливает выносливость со временем
    void RegenerateStamina()
    {
        if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    /// Двигает персонажа к цели и поворачивает его
    void MoveToTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f; // исключаем вертикаль

        float speed = moveSpeed * (isSprinting ? sprintMultiplier : 1f);
        Vector3 move = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime));
        }

        // Проверка достижения точки
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    /// Геттер для текущего уровня выносливости (например, для UI)
    public float GetStamina()
    {
        return currentStamina;
    }
}
