using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public enum Types{
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

    public string name;
    public int health;
    public int attack;
    public int defense;
    public string type;
    public int speed;

    public void PokemonInitializer(string name, int health, int attack, int defense, string type, int speed)
    {
        this.name = name;
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.type = type;
        this.speed = speed;

    }
}
