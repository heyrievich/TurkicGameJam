using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;

    [Header("Stamina Settings")]
    public float maxStamina = 5f;
    public float staminaRecoveryRate = 1f;
    public float staminaDrainRate = 1.5f;

    [Header("Respawn Settings")]
    public Transform spawnPoint;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip walkClip;
    public AudioClip runClip;

    [Header("TreeUp Flag")]
    public bool isTreeUp = false; // <-- новая публичная переменная

    private float currentStamina;
    private Rigidbody rb;
    private Animator animator;

    private bool isSprinting = false;
    private bool isTreeUpActive = false;

    private float sprintCooldownDuration = 1f;
    private float sprintCooldownTimer = 0f;

    private Vector3 inputDirection;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        currentStamina = maxStamina;

        if (spawnPoint == null)
            spawnPoint = transform;
    }

    void Update()
    {
        HandleMovementInput();
        HandleSprintInput();
        RegenerateStamina();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleMovementInput()
    {
        if (isTreeUpActive)
        {
            inputDirection = Vector3.zero;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal"); // A / D
        float v = Input.GetAxisRaw("Vertical");   // W / S

        inputDirection = new Vector3(h, 0f, v).normalized;
    }

    void MovePlayer()
    {
        if (inputDirection == Vector3.zero) return;

        float speed = moveSpeed * (isSprinting ? sprintMultiplier : 1f);
        Vector3 move = inputDirection * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);

        Quaternion toRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime));
    }

    void HandleSprintInput()
    {
        sprintCooldownTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0.1f && inputDirection != Vector3.zero)
        {
            if (sprintCooldownTimer <= 0f)
                isSprinting = true;

            currentStamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            if (isSprinting)
                sprintCooldownTimer = sprintCooldownDuration;

            isSprinting = false;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }

    void RegenerateStamina()
    {
        if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    void UpdateAnimation()
    {
        if (animator == null || isTreeUpActive) return;

        bool isMoving = inputDirection != Vector3.zero;

        animator.SetBool("isIdle", !isMoving);
        animator.SetBool("isWalk", isMoving && !isSprinting);
        animator.SetBool("isRun", isMoving && isSprinting);

        HandleFootstepSounds(isMoving);
    }

    void HandleFootstepSounds(bool isMoving)
    {
        if (!isMoving || isTreeUpActive)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            return;
        }

        AudioClip clip = isSprinting ? runClip : walkClip;
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            TeleportToSpawn();
        }
    }

    void TeleportToSpawn()
    {
        transform.position = spawnPoint.position;
        rb.velocity = Vector3.zero;
        inputDirection = Vector3.zero;
    }

    public float GetStamina() => currentStamina;

    // ------------ TREE UP ------------
    public void TreeUp()
    {
        StartCoroutine(TreeUpRoutine());
    }

    private IEnumerator TreeUpRoutine()
    {
        isTreeUpActive = true;
        isTreeUp = true; // <-- начало TreeUp

        inputDirection = Vector3.zero;

        if (animator != null)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
            animator.SetBool("isTreeUp", true);
        }

        yield return new WaitForSeconds(1.7f);

        if (animator != null)
            animator.SetBool("isTreeUp", false);

        isTreeUpActive = false;
        isTreeUp = false; // <-- окончание TreeUp

        transform.position += new Vector3(0, 0.05f, 0);
    }
}
