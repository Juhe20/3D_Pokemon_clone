using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravity = -9.8f;
    public LayerMask groundLayer;

    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    public bool inEvent = false;
    public Encounter encounter;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
    }

    void Update()
    {
        //Locks the cursor if the player is not in any dialog or battle event.
        if (!inEvent)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        //Free the cursor if the player enters an event.
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        HandleMovement();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        //Event check again as the player is not supposed to move during dialog or battles.
        if (!inEvent)
        {
            //Get input WASD/arrow key input to be able to move .
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            //Makes the player unable to move into the air and uses normalized to have normal diagonal movement.
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("isRunning", true);
                //Uses camera direction to know which direction the player should move.
                Camera mainCamera = Camera.main;
                Vector3 cameraForward = mainCamera.transform.forward;
                Vector3 cameraRight = mainCamera.transform.right;
                cameraForward.y = 0;
                cameraRight.y = 0;
                cameraForward.Normalize();
                cameraRight.Normalize();
                Vector3 moveDirection = cameraForward * direction.z + cameraRight * direction.x;

                //Makes the player rotate correctly when moving depending on camera movement.
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
        //Makes sure the player stays grounded at all times.
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
