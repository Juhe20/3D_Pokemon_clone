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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        BattleTextBox.SetActive(true);
        playerPokemon = playerPoke;
        enemyPokemon = enemyPoke;
        isBattleActive = true;
        movement.inEvent = true;
        animator.SetBool("isRunning", false);
        player.transform.LookAt(enemyPokemon.transform);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Update UI
        UpdateHealthUI();
        UpdateMoveButtons();
        StartCoroutine(DisplayBattleText($"You encountered a wild {enemyPokemon.pokemonName}", 2f));
        GameObject playerPokemonInstance = Instantiate(playerPoke.gameObject, player.position, Quaternion.identity);
        playerPokemonInstance.transform.position = new Vector3(player.position.x + 5f, player.position.y - 0.1f, player.position.z);
        playerPokemonInstance.transform.LookAt(enemyPokemon.transform);
        playerPokemonInstance.transform.SetParent(GameObject.Find("PokemonParent").transform);
        PikachuBehaviour pikachuFollowScript = playerPokemonInstance.GetComponent<PikachuBehaviour>();
        if (pikachuFollowScript != null)
        {
            pikachuFollowScript.enabled = false;
        }
        pikachu.SetActive(false);
    }

    private void UpdateHealthUI()
    {
        statusBarEnemy.SetActive(true);
        statusBarPlayer.SetActive(true);
        playerHealthText.text = $"{playerPokemon.pokemonName} HP: {playerPokemon.health}";
        enemyHealthText.text = $"{enemyPokemon.pokemonName} HP: {enemyPokemon.health}";
    }

    private void UpdateMoveButtons()
    {
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
        if (!isBattleActive) return;
        if (moveIndex < 0 || moveIndex >= playerPokemon.playerMoves.Count)
        {
            return;
        }
        Move selectedMove = playerPokemon.playerMoves[moveIndex];
        selectedMove.ExecuteMove(playerPokemon, enemyPokemon);
        StartCoroutine(DisplayBattleText($"{playerPokemon.pokemonName} used {selectedMove.moveName}!", 2f));
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
        yield return new WaitForSeconds(2f);
        Move enemyMove = enemyPokemon.ChooseRandomMove();
        enemyMove.ExecuteMove(enemyPokemon, playerPokemon);
        StartCoroutine(DisplayBattleText($"The wild {enemyPokemon.pokemonName} used {enemyMove.moveName}", 2f));
        UpdateHealthUI();
        if (playerPokemon.IsFainted())
        {
            battleText.text = $"Your {playerPokemon.pokemonName} has fainted";
            EndBattle();
        }
    }
    private IEnumerator DisplayBattleText(string message, float delay)
    {
        battleText.text = message;
        yield return new WaitForSeconds(delay);
    }

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
