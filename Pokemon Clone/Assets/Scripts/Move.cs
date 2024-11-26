using UnityEngine;
[System.Serializable]
public class Move
{
    public string moveName;
    public int power;
    public int pp;
    public Pokemon.Types moveType;
    public int accuracy;
    public Move(string name, int power, int pp, Pokemon.Types moveType, int accuracy)
    {
        this.moveName = name;
        this.power = power;
        this.pp = pp;
        this.moveType = moveType;
        this.accuracy = accuracy;
    }
    public void ExecuteMove(Pokemon attacker, Pokemon defender)
    {
        if (pp <= 0)
        {
            return;
        }
        if (Random.Range(0, 100) < accuracy)
        {
            int damage = (int)((attacker.attack * power) / defender.defense);
            defender.health -= damage;

            Debug.Log($"{attacker.name} used {moveName}! It dealt {damage} damage to {defender.name}.");
        }
        else
        {
            Debug.Log($"{attacker.name} used {moveName}, but it missed!");
        }
        pp--;
    }
}
