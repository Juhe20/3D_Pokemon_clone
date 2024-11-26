using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
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

    public string pokemonName;
    public int health;
    public int attack;
    public int defense;
    public Types type;
    public int speed;
    public GameObject gameObject;
    public List<Move> playerMoves = new List<Move>();
    public List<Move> enemyMovesPool = new List<Move>();


    public void PokemonInitializer(string name, int health, int attack, int defense, Types type, int speed, GameObject gameobject, List<Move> playerMoves, List<Move> enemyMovesPool)
    {
        this.pokemonName = name;
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.type = type;
        this.speed = speed;
        this.gameObject = gameobject;
        this.playerMoves = playerMoves;
        this.enemyMovesPool = enemyMovesPool;
    }
    public Move ChooseRandomMove()
    {
        return enemyMovesPool[Random.Range(0, enemyMovesPool.Count)];
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
    }
    public bool IsFainted()
    {
        return health <= 0;
    }
}
