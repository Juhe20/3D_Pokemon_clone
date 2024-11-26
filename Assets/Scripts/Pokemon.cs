using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    //Currently not in use but logic to calculate STAB moves and type defenses/offenses.
    public enum Types
    {
        Water,
        Fire,
        Ground,
        Rock,
        Poison,
        Steel,
        Fairy,
        Ghost,
        Dark,
        Grass,
        Bug,
        Electric,
        Psychic,
        Ice,
        Dragon,
        Fighting,
        Flying,
        Normal
    }

    //Variables that makes a Pokémon a Pokémon.
    public string pokemonName;
    public int maxHealth;
    public int health;
    public int attack;
    public int defense;
    public Types type;
    public int speed;
    public GameObject gameObject;
    //public lists of moves that can be changed in the inspector.
    public List<Move> playerMoves = new List<Move>();
    public List<Move> enemyMovesPool = new List<Move>();


    //Initialize Pokémons with specific stats.
    public void PokemonInitializer(string name, int maxHealth, int health, int attack, int defense, Types type, int speed, GameObject gameobject, List<Move> playerMoves, List<Move> enemyMovesPool)
    {
        this.pokemonName = name;
        this.maxHealth = maxHealth;
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.type = type;
        this.speed = speed;
        this.gameObject = gameobject;
        this.playerMoves = playerMoves;
        this.enemyMovesPool = enemyMovesPool;
    }

    //Function used by enemy Pokémon in battle.
    public Move ChooseRandomMove()
    {
        return enemyMovesPool[Random.Range(0, enemyMovesPool.Count)];
    }
    //Function that makes the specific Pokémon's health go down. Prevents it from going negative.
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
    }

    //Check if the Pokémon is dead.
    public bool IsFainted()
    {
        return health <= 0;
    }
}
