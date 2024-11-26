using UnityEngine;
//Serializable so the moves can be made in the inspector (ease of use)
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
        //Check if there's power points left on the move (Limited use) but not necessary this early with weak moves that has high pp.
        if (pp <= 0)
        {
            return;
        }
        //Accuracy check to make it true to the original game.
        if (Random.Range(0, 100) < accuracy)
        {
            //Damage calculations taking the attackers power and defenders defense into the formular.
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
