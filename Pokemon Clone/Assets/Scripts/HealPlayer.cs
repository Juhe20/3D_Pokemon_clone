using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public PlayerTeam team;
    public UIController healUI;
    public Animator animator;
    private bool isTextDisplayed;
    private bool playerTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            playerTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = false;
            healUI.HideHealText();
            isTextDisplayed = false;
            animator.SetBool("isTalking", false);
        }
    }

    private void Update()
    {
        // Check if the player is inside the trigger zone and presses the Space key
        if (playerTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isTalking", true);
            // Heal the player's team and update the UI
            foreach (var teamMember in team.team)
            {
                teamMember.health = teamMember.maxHealth;
            }
            healUI.ShowHealText("I have healed your party back up!");
            isTextDisplayed = true;
        }

        // Hide the UI and stop the animation when space is pressed again
        if (isTextDisplayed && Input.GetKeyDown(KeyCode.Space))
        {
            healUI.HideText();
            isTextDisplayed = false;
        }
    }
}
