using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f; // Horizontal movement speed
    public float gravity = -9.8f; // Gravity value (usually -9.8)
    private CharacterController characterController; // Reference to the CharacterController
    private Animator animator;

    void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
        characterController.stepOffset = 0.1f;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
    }

    void Update()
    {
        // Get input from WASD keys or arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a direction vector based on input
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Apply movement (no gravity on X and Z, only Y is affected by gravity)
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isRunning", true);

            // Get the main camera's forward and right directions
            Camera mainCamera = Camera.main;
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;

            // Set the y component to 0 for horizontal movement
            cameraForward.y = 0;
            cameraRight.y = 0;

            // Normalize the vectors to ensure consistent movement speed
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate the desired move direction relative to the camera's orientation
            Vector3 moveDirection = cameraForward * direction.z + cameraRight * direction.x;

            // Rotate the player to face the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);

            // Move the character horizontally (X and Z axes)
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

}