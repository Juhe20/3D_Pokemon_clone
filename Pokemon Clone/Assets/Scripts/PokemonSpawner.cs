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
    void Start()
    {
        bushes = new List<Bush>(FindObjectsOfType<Bush>());
    }

    void Update()
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

    void SpawnPokemon(Transform spawnLocation)
    {
        // 50% chance to spawn a Pokémon
        if (Random.value <= 0.5f)
        {
            SpawnPokemonNearBush(spawnLocation);
        }
    }

    void SpawnPokemonNearBush(Transform spawnLocation)
    {
        // Instantiate a random Pokémon prefab
        GameObject spawnedPokemon = Instantiate(
            pokemonPrefabs[Random.Range(0, pokemonPrefabs.Length)],
            spawnLocation.position,
            Quaternion.identity
        );

        // Track the spawned Pokémon
        activePokemon[spawnLocation] = spawnedPokemon;
    }

    void DespawnPokemonAtLocation(Transform spawnLocation)
    {
        // Destroy the Pokémon GameObject
        if (activePokemon.TryGetValue(spawnLocation, out GameObject pokemon))
        {
            Destroy(pokemon);
            activePokemon.Remove(spawnLocation);
        }
    }
}
