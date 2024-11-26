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
    void Start()
    {
        //Finds all grass patches in the scene.
        bushes = new List<Bush>(FindObjectsOfType<Bush>());
    }

    void Update()
    {
        //Checks the list of bushes it found in the scene for spawn locations
        foreach (var bush in bushes)
        {
            foreach (var spawnLocation in bush.spawnLocations)
            {
                //Checks if the player is within range to spawn a Pokémon on the spawn point.
                float distance = Vector3.Distance(player.position, spawnLocation.position);
                if (distance <= spawnRange)
                {
                    //Check to see if the player has already spawned something on the spawnlocation. Prevents multiple spawns on the same spot.
                    if (!visitedLocations.Contains(spawnLocation))
                    {
                        visitedLocations.Add(spawnLocation);
                        SpawnPokemon(spawnLocation);
                    }
                }
                //If the player moves outside the spawn range the spawned Pokémon is destroyed and the location can be visited again to spawn a new one.
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
        //Random value is between 0.0 and 1. Means that this check should go through 50% of the time.
        if (Random.value <= 0.5f)
        {
            SpawnPokemonNearBush(spawnLocation);
        }
    }

    void SpawnPokemonNearBush(Transform spawnLocation)
    {
        //Spawns a random Pokémon prefab from a list of prefabs. Spawns it on the spawn location that the player is in range of.
        GameObject spawnedPokemon = Instantiate(pokemonPrefabs[Random.Range(0, pokemonPrefabs.Length)], spawnLocation.position, Quaternion.identity);
        //Makes it a child of the empty Pokemon parent to easily remove at any point if needed.
        spawnedPokemon.transform.SetParent(GameObject.Find("PokemonParent").transform);
        //Dictionary to have a Pokémon GameObject stored together with a spawn location.
        activePokemon[spawnLocation] = spawnedPokemon;
    }

    void DespawnPokemonAtLocation(Transform spawnLocation)
    {
        //Check the dictionary to see if a Pokémon is spawned on the spawn location.
        //Destroys the Pokémon as this method will only be called when the player moves outside the range of a spawn location.
        if (activePokemon.TryGetValue(spawnLocation, out GameObject pokemon))
        {
            Destroy(pokemon);
            activePokemon.Remove(spawnLocation);
        }
    }
}
