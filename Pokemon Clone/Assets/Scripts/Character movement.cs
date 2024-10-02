using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController characterController; // Reference to the CharacterController
    public float speed = 5.0f; // Movement speed

    void Update()
    {
        // Get input from WASD keys or arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a direction vector based on input
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Check if there's any input
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

            // Move the character
            characterController.Move(moveDirection * speed * Time.deltaTime);

            // Optional: Rotate the character to face the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
