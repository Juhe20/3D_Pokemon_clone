using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using Cinemachine;

public class BattleManager : MonoBehaviour
{
    public Pokemon playerPokemon;
    public Pokemon enemyPokemon;
    public bool isBattleActive = false;
    public Transform player;
    private Encounter encounterScript;
    public CharacterMovement movement;
    public Animator animator;
    public GameObject pikachu;
    // UI Elements
    public GameObject statusBarPlayer;
    public GameObject statusBarEnemy;
    public GameObject BattleTextBox;
    public List<Button> moveButtons;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI battleText;
    public ClearPokemon clearPokemon;
    public Move move;

    private void Start()
    {
        //Make sure we aren't already in a battle, find the encounter script for later and add listener to UI buttons.
        isBattleActive = false;
        encounterScript = FindObjectOfType<Encounter>();
        for (int i = 0; i < moveButtons.Count; i++)
        {
            int index = i;
            moveButtons[i].onClick.AddListener(() => OnMoveButtonClicked(index));
        }
    }
    public void StartBattle(Pokemon playerPoke, Pokemon enemyPoke)
    {

        //Enable UI
        BattleTextBox.SetActive(true);
        //Assign the Pokémon on the field to the Pokémon encountered.
        playerPokemon = playerPoke;
        enemyPokemon = enemyPoke;
        //Check to say we're in battle and in event so the player can't move around the battle area.
        isBattleActive = true;
        movement.inEvent = true;
        animator.SetBool("isRunning", false);
        //Make the player look at the opposing Pokémon.
        player.transform.LookAt(enemyPokemon.transform);
        //Unlock cursor so move buttons can be clicked.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Enable health UI and move buttons.
        UpdateHealthUI();
        UpdateMoveButtons();
        //Text changed on the UI to tell the player which Pokémon they encountered.
        StartCoroutine(DisplayBattleText($"You encountered a wild {enemyPokemon.pokemonName}", 2f));
        //Instantiate GameObject from the first slot in the player's team.
        GameObject playerPokemonInstance = Instantiate(playerPoke.gameObject, player.position, Quaternion.identity);
        //Change it's position and look direction. Make it a child of the PokemonParent empty object to easily remove later.
        playerPokemonInstance.transform.position = new Vector3(player.position.x + 5f, player.position.y - 0.1f, player.position.z);
        playerPokemonInstance.transform.LookAt(enemyPokemon.transform);
        playerPokemonInstance.transform.SetParent(GameObject.Find("PokemonParent").transform);
        //As Pikachu is the only available Pokémon it's always in the first slot and has the follow behaviour on.
        //Disable it so it doesn't follow the player in the battle area.
        PikachuBehaviour pikachuFollowScript = playerPokemonInstance.GetComponent<PikachuBehaviour>();
        if (pikachuFollowScript != null)
        {
            pikachuFollowScript.enabled = false;
        }
        //Set the following Pikachu to inactive (while battling) since we already spawned a new instance of it in the battle area.
        pikachu.SetActive(false);
    }

    //Function to update the health UI text and panel.
    private void UpdateHealthUI()
    {
        statusBarEnemy.SetActive(true);
        statusBarPlayer.SetActive(true);
        playerHealthText.text = $"{playerPokemon.pokemonName} HP: {playerPokemon.health}";
        enemyHealthText.text = $"{enemyPokemon.pokemonName} HP: {enemyPokemon.health}";
    }

    private void UpdateMoveButtons()
    {
        //Loops through the buttons and finds the text children to update their names to the corresponding Pokémon move from the move list.
        for (int i = 0; i < moveButtons.Count; i++)
        {
            if (i < playerPokemon.playerMoves.Count)
            {
                moveButtons[i].gameObject.SetActive(true);
                moveButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerPokemon.playerMoves[i].moveName;
            }
            else
            {
                moveButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnMoveButtonClicked(int moveIndex)
    {
        //Checks the button clicked and sets the selected move to which move the button contains.
        Move selectedMove = playerPokemon.playerMoves[moveIndex];
        //Executes the move where the playerPokemon is the attacker and the enemyPokemon is the defender.
        selectedMove.ExecuteMove(playerPokemon, enemyPokemon);
        //Update text and health after damaging the enemy.
        StartCoroutine(DisplayBattleText($"{playerPokemon.pokemonName} used {selectedMove.moveName}!", 2f));
        UpdateHealthUI();
        //Checks if the enemy died from the attack so the battle can end.
        //If it isn't dead the battle continues with the turn going to the enemy.
        if (enemyPokemon.IsFainted())
        {
            StartCoroutine(DisplayBattleText($"Enemy {enemyPokemon.pokemonName} fainted!", 2f));
            EndBattle();
            return;
        }
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        //Same function as the OnMoveButtonClicked apart from the enemy randomly choosing the move.
        yield return new WaitForSeconds(2f);
        Move enemyMove = enemyPokemon.ChooseRandomMove();
        //Executes the move again but with the enemy being the attacker and playerPokemon being the defender.
        enemyMove.ExecuteMove(enemyPokemon, playerPokemon);
        StartCoroutine(DisplayBattleText($"The wild {enemyPokemon.pokemonName} used {enemyMove.moveName}", 2f));
        UpdateHealthUI();
        if (playerPokemon.IsFainted())
        {
            battleText.text = $"Your {playerPokemon.pokemonName} has fainted";
            EndBattle();
        }
    }        
    //Changes the text of the battle panel when called with a message.
    private IEnumerator DisplayBattleText(string message, float delay)
    {

        battleText.text = message;
        yield return new WaitForSeconds(delay);
    }

    //Returns all bools to false so we know we aren't in a battle anymore.
    //Uses the encounter script's function ReturnToOriginalPosition() to get the position before the player was teleport from the playerpreferences.
    //Destroys all Pokémon in the Pokémon parent GameObject to make sure there isn't any Pokémon left in the battle area after ending battle.
    public void EndBattle()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isBattleActive = false;
        BattleTextBox.SetActive(false);
        statusBarPlayer.SetActive(false);
        statusBarEnemy.SetActive(false);
        encounterScript.ReturnToOriginalPosition();
        clearPokemon.ClearAllPokemon();
        pikachu.SetActive(true);
        movement.inEvent = false;
    }
}
