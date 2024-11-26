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
        //Sets a bool if the player is in the trigger zone. Makes it easier to handle in the update function.
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            playerTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Turns all UI off and set the bool to falkse again if the player walks out of range.
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
        //Checks if the player is in the trigger zone and space is clicked.
        if (playerTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isTalking", true);
            // Heal the player's team and update the UI text
            foreach (var teamMember in team.team)
            {
                teamMember.health = teamMember.maxHealth;
            }
            healUI.ShowHealText("I have healed your party back up!");
            isTextDisplayed = true;
        }
        // Hide the UI when space is pressed again.
        if (isTextDisplayed && Input.GetKeyDown(KeyCode.Space))
        {
            healUI.HideText();
            isTextDisplayed = false;
        }
    }
}
