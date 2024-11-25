using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f; // Horizontal movement speed
    public float gravity = -9.8f; // Gravity value
    public LayerMask groundLayer; // Layer for ground objects

    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity; // Tracks the player's current velocity
    public bool inEvent = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
    }

    void Update()
    {
        HandleMovement();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        if (!inEvent)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("isRunning", true);

                Camera mainCamera = Camera.main;
                Vector3 cameraForward = mainCamera.transform.forward;
                Vector3 cameraRight = mainCamera.transform.right;

                cameraForward.y = 0;
                cameraRight.y = 0;
                cameraForward.Normalize();
                cameraRight.Normalize();

                Vector3 moveDirection = cameraForward * direction.z + cameraRight * direction.x;

                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

                characterController.Move(moveDirection * speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }

    private void ApplyGravity()
    {
        // Check if the player is grounded
        bool isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            // Reset vertical velocity when grounded
            velocity.y = -2f; // Small value to keep the player "stuck" to the ground
        }

        // Apply gravity to the velocity
        velocity.y += gravity * Time.deltaTime;

        // Apply the velocity to the player
        characterController.Move(velocity * Time.deltaTime);
    }
}
