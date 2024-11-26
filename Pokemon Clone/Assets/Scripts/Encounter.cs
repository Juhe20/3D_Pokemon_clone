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
        GameObject findPlayer = GameObject.FindWithTag("Player");
        player = findPlayer.transform;
        camera = FindObjectOfType<CinemachineFreeLook>();
        teleportPoint = GameObject.Find("BattleScene").transform;
        battleManager = FindObjectOfType<BattleManager>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
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
        Debug.Log("Storing original position...");
        PlayerPrefs.SetFloat("OriginalPlayerPosX", player.position.x);
        PlayerPrefs.SetFloat("OriginalPlayerPosY", player.position.y);
        PlayerPrefs.SetFloat("OriginalPlayerPosZ", player.position.z);
        PlayerPrefs.Save();
    }

    public void RetrieveOriginalPosition()
    {
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
        PlayerPrefs.DeleteKey("OriginalPlayerPosX");
        PlayerPrefs.DeleteKey("OriginalPlayerPosY");
        PlayerPrefs.DeleteKey("OriginalPlayerPosZ");
        PlayerPrefs.Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!HasAvailablePokemon())
            {
                Debug.Log("Player has no available Pokémon! Encounter canceled.");
                return;
            }
            var movementScript = other.GetComponent<CharacterController>();
            movementScript.enabled = false;
            originalPlayerPosition = player.position;
            StoreOriginalPosition(); 
            TeleportPlayer();
            movementScript.enabled = true;
            GameObject instance = Instantiate(pokemonPrefab, player.position, Quaternion.identity);
            instance.transform.position = new Vector3(player.position.x + 15f, player.position.y - 0.1f, player.position.z);
            instance.transform.LookAt(player.position);
            instance.transform.SetParent(GameObject.Find("PokemonParent").transform);
            inBattle = true;
            Pokemon enemyPokemon = instance.GetComponent<Pokemon>();
            Pokemon playerPokemon = player.GetComponent<PlayerTeam>().GetFirstAvailablePokemon();
            battleManager.StartBattle(playerPokemon, enemyPokemon);
        }
    }


    public void ReturnToOriginalPosition()
    {
        RetrieveOriginalPosition();

        var movementScript = player.GetComponent<CharacterController>();
        movementScript.enabled = false;
        player.position = originalPlayerPosition;
        Vector3 positionOffset = player.position - originalPlayerPosition;
        camera.OnTargetObjectWarped(player, positionOffset);
        camera.ForceCameraPosition(player.position, camera.transform.rotation);
        movementScript.enabled = true;
        ClearStoredPosition();
    }

    public void TeleportPlayer()
    {
        player.position = teleportPoint.position;
        Vector3 positionOffset = player.position - originalPlayerPosition;
        camera.OnTargetObjectWarped(player, positionOffset);
        camera.ForceCameraPosition(player.position, camera.transform.rotation);
    }
    private IEnumerator StartChase()
    {
        isLooking = true;
        transform.LookAt(player.position);
        yield return new WaitForSeconds(chaseDelay);
        isChasing = true;
        isLooking = false;
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        transform.position += direction * speed * Time.deltaTime;
    }
    private bool HasAvailablePokemon()
{
    PlayerTeam playerTeam = player.GetComponent<PlayerTeam>();
    if (playerTeam == null)
    {
        Debug.LogError("PlayerTeam component not found on the player!");
        return false;
    }

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
