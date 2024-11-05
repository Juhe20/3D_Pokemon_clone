using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSpawner : MonoBehaviour
{
    public GameObject[] pokemonPrefabs;
    public Transform[] spawnLocations;
    void SpawnPokemon(string name, int health, int attack, int defense, string type, int speed)
    {
        GameObject pokemonPrefab = pokemonPrefabs[Random.Range(0, pokemonPrefabs.Length)];
    }

}
