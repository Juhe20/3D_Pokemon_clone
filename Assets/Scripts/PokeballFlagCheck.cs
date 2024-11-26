using UnityEngine;

public class PokeballFlagCheck : MonoBehaviour
{
    public GameObject pokeball;
    public UIController textDisplay;
    private bool playerTrigger;
    public CharacterMovement movement;
    public bool isTextDisplayed = false;
    private int dialoguePage = 1;
    public Animator proffessorOak;
    public Animator Player;
    public Transform oakPosition;
    public Transform playerPosition;
    public bool isPikachuRecieved = false;
    public GameObject Pikachu;
    public PlayerTeam playerTeam;


    private void OnTriggerEnter(Collider other)
    {
        //Checks if the player enters the trigger collider and sets bool to true.
        //Makes it easier to constantly look for keypresses in Update()
        if (other.CompareTag("Player"))
        {
            playerTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //Sets everything inactive again if the player moves outside the trigger collider
        if (other.CompareTag("Player"))
        {
            playerTrigger = false;
            textDisplay.HideText();
            isTextDisplayed = false;
        }
    }

    private void Update()
    {
        //Checks if the player is in range, clicks space, has no dialog active and has not acquired Pikachu yet.
        if (playerTrigger && Input.GetKeyDown(KeyCode.Space) && !isTextDisplayed && !isPikachuRecieved)
        {
            //Make the NPC look at the player and start a talk animation. Activates dialog panels with text.
            oakPosition.LookAt(playerPosition.transform.position);
            Player.SetBool("isRunning", false);
            proffessorOak.SetBool("isTalking", true);
            movement.inEvent = true;
            textDisplay.ShowText("The other trainers already picked their Pokémon so the only one left is this Pikachu!");
            isTextDisplayed = true;
        }
        //Click space to get to the next text page.
        else if (isTextDisplayed && Input.GetKeyDown(KeyCode.Space) && dialoguePage == 1)
        {
            textDisplay.ShowText("You received Pikachu!");
            dialoguePage++;
        }
        else if (isTextDisplayed && Input.GetKeyDown(KeyCode.Space) && dialoguePage == 2)
        {
            //Sets everything back to false and checks off the Pikachu acquired flag. Adds Pikachu to the players team list.
            proffessorOak.SetBool("isTalking", false);
            movement.inEvent = false;
            textDisplay.HideText();
            isTextDisplayed = false;
            dialoguePage = 0;
            pokeball.SetActive(false);
            isPikachuRecieved = true;
            Pikachu.SetActive(true);
            AddPikachuToTeam();
        }
    }
    private void AddPikachuToTeam()
    {
        //Check the Pokémon script on the Pikachu GameObject and that specific Pikachu Pokémon to the players team.
        Pokemon pikachu = Pikachu.GetComponent<Pokemon>();
        playerTeam.AddPokemon(pikachu);
    }
}
