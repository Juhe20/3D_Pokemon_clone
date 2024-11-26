using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public UIController UI;
    public List<Pokemon> team = new List<Pokemon>();
    private bool isTextDisplayed = false;

    public void AddPokemon(Pokemon newPokemon)
    {
        if (team.Count < 6) 
        {
            team.Add(newPokemon);
            UI.ShowText($"{newPokemon.name} has been added to your party!");
            isTextDisplayed = true;
            if (isTextDisplayed && Input.GetKeyDown(KeyCode.Space))
            {
                UI.HideText();
                isTextDisplayed = false;
            }
        }
        else
        {
            UI.ShowText($"Your party is full! {newPokemon.name} has been send to your box");
        }
    }

    public Pokemon GetFirstAvailablePokemon()
    {
        foreach (var pokemon in team)
        {
            if (pokemon.health > 0)
            {
                return pokemon;
            }
        }
        return null;
    }
}
