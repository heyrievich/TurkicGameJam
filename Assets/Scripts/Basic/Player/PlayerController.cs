using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public LayerMask groundLayer;

    [Header("Stamina Settings")]
    public float maxStamina = 5f;
    public float staminaRecoveryRate = 1f;
    public float staminaDrainRate = 1.5f;

    private float currentStamina;
    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isSprinting = false;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        currentStamina = maxStamina;
    }

    void Update()
    {
        HandleMouseClick();
        HandleSprintInput();
        RegenerateStamina();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

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

    void HandleSprintInput()
    {
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

    void RegenerateStamina()
    {
        if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    void MoveToTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f;

        float speed = moveSpeed * (isSprinting ? sprintMultiplier : 1f);
        Vector3 move = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime));
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    void UpdateAnimation()
    {
        if (animator == null) return;

        animator.SetBool("isIdle", !isMoving);
        animator.SetBool("isWalk", isMoving && !isSprinting);
        animator.SetBool("isRun", isMoving && isSprinting);
    }

    public float GetStamina()
    {
        return currentStamina;
    }
}
