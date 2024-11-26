using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSpawner : MonoBehaviour
{
    public GameObject[] pokemonPrefabs;
    public List<Bush> bushes;
    public float spawnRange = 10f;
    public Transform player;
    private Dictionary<Transform, GameObject> activePokemon = new Dictionary<Transform, GameObject>();
    private HashSet<Transform> visitedLocations = new HashSet<Transform>();
    private float battleCooldown = 2f;
    private bool battleEnded = true;
    private float cooldownTimer = 0f;
    void Start()
    {
        bushes = new List<Bush>(FindObjectsOfType<Bush>());
    }

    void Update()
    {
        if (!battleEnded)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                battleEnded = true; // Allow Pokémon spawning again
            }
        }
        if (battleEnded)
        {
            foreach (var bush in bushes)
            {
                foreach (var spawnLocation in bush.spawnLocations)
                {
                    float distance = Vector3.Distance(player.position, spawnLocation.position);
                    if (distance <= spawnRange)
                    {
                        if (!visitedLocations.Contains(spawnLocation))
                        {
                            visitedLocations.Add(spawnLocation);
                            SpawnPokemon(spawnLocation);
                        }
                    }
                    else
                    {
                        if (visitedLocations.Contains(spawnLocation))
                        {
                            visitedLocations.Remove(spawnLocation);
                            DespawnPokemonAtLocation(spawnLocation);
                        }
                    }
                }
            }
        }

    }

    void SpawnPokemon(Transform spawnLocation)
    {
        if (Random.value <= 0.5f)
        {
            SpawnPokemonNearBush(spawnLocation);
        }
    }
    public void EndBattle()
    {
        battleEnded = false;
        cooldownTimer = battleCooldown;  // Start cooldown timer
        StartCoroutine(BattleEndCooldown());
    }

    void SpawnPokemonNearBush(Transform spawnLocation)
    {
        GameObject spawnedPokemon = Instantiate(pokemonPrefabs[Random.Range(0, pokemonPrefabs.Length)], spawnLocation.position, Quaternion.identity);
        spawnedPokemon.transform.SetParent(GameObject.Find("PokemonParent").transform);
        activePokemon[spawnLocation] = spawnedPokemon;
    }

    void DespawnPokemonAtLocation(Transform spawnLocation)
    {
        if (activePokemon.TryGetValue(spawnLocation, out GameObject pokemon))
        {
            Destroy(pokemon);
            activePokemon.Remove(spawnLocation);
        }
    }
    private IEnumerator BattleEndCooldown()
    {
        // Wait for the cooldown to finish
        yield return new WaitForSeconds(battleCooldown);
        battleEnded = true; // Allow Pokémon spawning again
    }
}
