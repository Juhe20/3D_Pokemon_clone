using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f; // Horizontal movement speed
    public float gravity = -9.8f; // Gravity value (usually -9.8)
    public float jumpHeight = 2.0f; // Jump height (optional, if you want jumping)

    private CharacterController characterController; // Reference to the CharacterController
    private Vector3 velocity; // Store current velocity (including gravity)

    void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
        characterController.stepOffset = 0.5f; // Adjust based on terrain size
        Cursor.lockState = CursorLockMode.Locked;

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

            // Move the character horizontally (X and Z axes)
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }

        // Gravity and falling
        if (characterController.isGrounded)
        {
            velocity.y = 0f; // Reset vertical velocity when grounded

            // Optional: Handle jumping if needed
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Jump if grounded
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // Apply gravity when in the air
        }

        // Apply the vertical velocity (Y-axis) to the character's movement
        characterController.Move(velocity * Time.deltaTime);
    }
}
