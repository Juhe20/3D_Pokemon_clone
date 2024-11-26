using Cinemachine;
using System.Collections;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public Transform teleportPoint;
    public Transform player;
    public CinemachineFreeLook camera;
    private float lookDistance = 5f;
    public float speed = 2f;
    private float chaseDelay = 2f;
    private bool isChasing = false;
    private bool isLooking = false;
    public GameObject pokemonPrefab;
    public bool inBattle = false;
    private BattleManager battleManager;
    private Vector3 originalPlayerPosition;


    private void Start()
    {
        //Get objects as the script is on a prefab so can't manually assign in the inspector.
        GameObject findPlayer = GameObject.FindWithTag("Player");
        player = findPlayer.transform;
        camera = FindObjectOfType<CinemachineFreeLook>();
        teleportPoint = GameObject.Find("BattleScene").transform;
        battleManager = FindObjectOfType<BattleManager>();
    }

    private void Update()
    {
        //Calculate distance between the Pokémon and the player.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //Start looking at the player first, then chase afterwards.
        if (distanceToPlayer < lookDistance && !isChasing && !isLooking)
        {
            StartCoroutine(StartChase());
        }
        if (isChasing && distanceToPlayer < lookDistance)
        {
            ChasePlayer();
        }
    }
    public void StoreOriginalPosition()
    {
        //Store the player position. Used to teleport the player back to where they were before entering a battle.
        PlayerPrefs.SetFloat("OriginalPlayerPosX", player.position.x);
        PlayerPrefs.SetFloat("OriginalPlayerPosY", player.position.y);
        PlayerPrefs.SetFloat("OriginalPlayerPosZ", player.position.z);
        PlayerPrefs.Save();
    }

    public void RetrieveOriginalPosition()
    {
        //Retrieve the player position from the player prefs.
        if (PlayerPrefs.HasKey("OriginalPlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("OriginalPlayerPosX");
            float y = PlayerPrefs.GetFloat("OriginalPlayerPosY");
            float z = PlayerPrefs.GetFloat("OriginalPlayerPosZ");
            originalPlayerPosition = new Vector3(x, y, z);
        }
    }
    public void ClearStoredPosition()
    {
        //Delete any position the player pref is currently holding.
        //Makes the player able to store a new position again when entering a battle at another spot.
        PlayerPrefs.DeleteKey("OriginalPlayerPosX");
        PlayerPrefs.DeleteKey("OriginalPlayerPosY");
        PlayerPrefs.DeleteKey("OriginalPlayerPosZ");
        PlayerPrefs.Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Collision between the player and the Pokémon.
        if (other.CompareTag("Player"))
        {
            //Check if the player has Pokémon with more than 0 health in their party.
            if (!HasAvailablePokemon())
            {
                Debug.Log("Player has no available Pokémon! Encounter canceled.");
                return;
            }
            //Disable the player movement script temporarily so the character controller doesn't mess up the teleport.
            var movementScript = other.GetComponent<CharacterController>();
            movementScript.enabled = false;
            originalPlayerPosition = player.position;
            //Store the position in the player prefs.
            StoreOriginalPosition(); 
            //Teleport the player to the battle area and reenable the movement script.
            TeleportPlayer();
            movementScript.enabled = true;
            //Make a new instance of whichever Pokémon the player collided with to enter the encounter.
            GameObject instance = Instantiate(pokemonPrefab, player.position, Quaternion.identity);
            instance.transform.position = new Vector3(player.position.x + 15f, player.position.y - 0.1f, player.position.z);
            //Make it look at the player from the new position and set the parent to PokemonParent as with all other spawns.
            instance.transform.LookAt(player.position);
            instance.transform.SetParent(GameObject.Find("PokemonParent").transform);
            inBattle = true;
            //Assign an enemy and player Pokémon to get the stats and moves for them when going into battle.
            Pokemon enemyPokemon = instance.GetComponent<Pokemon>();
            Pokemon playerPokemon = player.GetComponent<PlayerTeam>().GetFirstAvailablePokemon();
            //Start the battle with the 2 Pokémon.
            battleManager.StartBattle(playerPokemon, enemyPokemon);
        }
    }


    public void ReturnToOriginalPosition()
    {
        //Function used after battle ends. Gets the position from player prefs to teleport the player back to the correct spot where they left.
        RetrieveOriginalPosition();
        //Disable movement again temporarily so the character controller lets the player teleport correctly.
        var movementScript = player.GetComponent<CharacterController>();
        movementScript.enabled = false;
        //Set the player position to the stored one and let the camera teleport smoothly with the player.
        player.position = originalPlayerPosition;
        Vector3 positionOffset = player.position - originalPlayerPosition;
        camera.OnTargetObjectWarped(player, positionOffset);
        camera.ForceCameraPosition(player.position, camera.transform.rotation);
        //Reenable the movement and clear the position as the player now needs to set a new position if they enter another encounter.
        movementScript.enabled = true;
        ClearStoredPosition();
    }

    public void TeleportPlayer()
    {
        //Teleports the player to a GameObject inside the battle area.
        player.position = teleportPoint.position;
        Vector3 positionOffset = player.position - originalPlayerPosition;
        camera.OnTargetObjectWarped(player, positionOffset);
        camera.ForceCameraPosition(player.position, camera.transform.rotation);
    }
    private IEnumerator StartChase()
    {
        //Delay to make the Pokémon chasing the player feel a bit more natural instead of instantly chasing the player down when in range.
        isLooking = true;
        transform.LookAt(player.position);
        yield return new WaitForSeconds(chaseDelay);
        isChasing = true;
        isLooking = false;
    }

    private void ChasePlayer()
    {
        //Move the Pokémon towards to player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        transform.position += direction * speed * Time.deltaTime;
    }
    private bool HasAvailablePokemon()
    {
        //Check to see if the player has any Pokémon in their party with over 0 health. Prevents encounters where the player has nothing to battle with.
        PlayerTeam playerTeam = player.GetComponent<PlayerTeam>();
        foreach (var pokemon in playerTeam.team)
        {
            if (pokemon.health > 0)
            {
                return true;
            }
        }
        return false;
    }


}
