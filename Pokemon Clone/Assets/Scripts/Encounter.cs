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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var movementScript = other.GetComponent<CharacterController>();
            movementScript.enabled = false;
            if (PlayerPrefs.GetFloat("OriginalPlayerPosX", -1f) == -1f)
            {
                StoreOriginalPosition();
            }
            TeleportPlayer();
            movementScript.enabled = true;
            GameObject instance = Instantiate(pokemonPrefab, player.position, Quaternion.identity);
            instance.transform.position = new Vector3(player.position.x + 15f, player.position.y - 0.1f, player.position.z);
            instance.transform.LookAt(player.position);
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

}
