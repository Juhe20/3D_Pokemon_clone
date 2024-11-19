using System.Collections.Generic;
using UnityEngine;

public class PokemonSpawner : MonoBehaviour
{
    public GameObject pokemonPrefab;
    public List<Bush> bushes;
    public float spawnRange = 5f;

    public Transform player;

    private HashSet<Transform> spawnedLocations = new HashSet<Transform>();

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
                if (Vector3.Distance(player.position, spawnLocation.position) <= spawnRange)
                {
                    if (!spawnedLocations.Contains(spawnLocation))
                    {
                        SpawnPokemonNearBush(spawnLocation);
                        spawnedLocations.Add(spawnLocation); // Prevent multiple spawns
                    }
                }
                else
                {
                    spawnedLocations.Remove(spawnLocation); // Allow re-spawning if player leaves range
                }
            }
        }
    }

    void SpawnPokemonNearBush(Transform spawnLocation)
    {
        Instantiate(pokemonPrefab, spawnLocation.position, Quaternion.identity);
    }
}
