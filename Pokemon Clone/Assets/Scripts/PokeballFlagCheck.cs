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
        if (other.CompareTag("Player"))
        {
            playerTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = false;
            textDisplay.HideText();
            isTextDisplayed = false;
        }
    }

    private void Update()
    {
        if (playerTrigger && Input.GetKeyDown(KeyCode.Space) && !isTextDisplayed && !isPikachuRecieved)
        {
            oakPosition.LookAt(playerPosition.transform.position);
            Player.SetBool("isRunning", false);
            proffessorOak.SetBool("isTalking", true);
            movement.inEvent = true;
            textDisplay.ShowText("The other trainers already picked their Pokémon so the only one left is this Pikachu!");
            isTextDisplayed = true;
        }
        else if (isTextDisplayed && Input.GetKeyDown(KeyCode.Space) && dialoguePage == 1)
        {
            textDisplay.ShowText("You received Pikachu!");
            dialoguePage++;
        }
        else if (isTextDisplayed && Input.GetKeyDown(KeyCode.Space) && dialoguePage == 2)
        {
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
        Pokemon pikachu = Pikachu.GetComponent<Pokemon>();
        playerTeam.AddPokemon(pikachu);
    }
}
