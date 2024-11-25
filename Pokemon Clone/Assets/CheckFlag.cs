using UnityEngine;

public class CheckFlag : MonoBehaviour
{
    public Transform player;
    public UIController UI;
    public CharacterMovement movement;
    public Animator playerAnimator;
    private bool isTextDisplayed = false;

    private void OnTriggerEnter(Collider other)
    {
        // Trigger event only once when the player enters the collider
        if (other.CompareTag("Player") && !isTextDisplayed)
        {
            player.transform.position = new Vector3(player.transform.position.x - 5f, player.transform.position.y, player.transform.position.z);
            playerAnimator.SetBool("isRunning", false);
            movement.inEvent = true;
            UI.ShowText("I should probably visit Professor Oak before I venture into the wild");
            isTextDisplayed = true;
        }
    }

    private void Update()
    {
        // Check if text is displayed and the spacebar is pressed
        if (isTextDisplayed && Input.GetKeyDown(KeyCode.Space))
        {
            // Hide text and reset state
            UI.HideText();
            movement.inEvent = false;
            isTextDisplayed = false;
        }
    }
}
