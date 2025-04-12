using UnityEngine;
using System.Collections;

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

    [Header("Respawn Settings")]
    public Transform spawnPoint; //  Новая переменная

    private float currentStamina;
    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isSprinting = false;

    private float sprintCooldownDuration = 1f;
    private float sprintCooldownTimer = 0f;
    private bool isTreeUpActive = false;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip walkClip;
    public AudioClip runClip;


    private Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        currentStamina = maxStamina;

        if (spawnPoint == null)
            spawnPoint = transform; // Если не задано — использовать текущую позицию как старт
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
        if (isTreeUpActive) return;
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
        sprintCooldownTimer -= Time.deltaTime;

        if (Input.GetMouseButton(1) && currentStamina > 0.1f && isMoving)
        {
            if (sprintCooldownTimer <= 0f)
            {
                isSprinting = true;
            }

            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else
        {
            if (isSprinting)
                sprintCooldownTimer = sprintCooldownDuration;

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
        if (animator == null || isTreeUpActive) return;


        animator.SetBool("isIdle", !isMoving);
        animator.SetBool("isWalk", isMoving && !isSprinting);
        animator.SetBool("isRun", isMoving && isSprinting);

        HandleFootstepSounds();
    }

    void HandleFootstepSounds()
    {
        if (!isMoving || isTreeUpActive)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            return;
        }

        AudioClip targetClip = isSprinting ? runClip : walkClip;

        if (audioSource.clip != targetClip)
        {
            audioSource.clip = targetClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            Debug.Log("Касание с лавой");
            TeleportToSpawn();
        }
    }

    void TeleportToSpawn()
    {
        transform.position = spawnPoint.position;
        rb.velocity = Vector3.zero; // сброс скорости
        isMoving = false;
        targetPosition = spawnPoint.position;
    }

    public float GetStamina()
    {
        return currentStamina;
    }


    public void TreeUp()
    {
        StartCoroutine(TreeUpRoutine());
    }

    private IEnumerator TreeUpRoutine()
    {
        isTreeUpActive = true;
        // Блокируем движение
        bool prevIsMoving = isMoving;
        isMoving = false;

        // Отключаем текущие анимации
        if (animator != null)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
            animator.SetBool("isTreeUp", true);
        }

        yield return new WaitForSeconds(1.7f);

        // Возвращаем флаги в норму
        if (animator != null)
        {
            animator.SetBool("isTreeUp", false);
        }

        // Возвращаем возможность двигаться, если была включена
        isMoving = prevIsMoving;
        isTreeUpActive = false;
    }
}
