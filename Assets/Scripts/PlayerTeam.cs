using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public UIController UI;
    public List<Pokemon> team = new List<Pokemon>();
    private bool isTextDisplayed = false;

    public void AddPokemon(Pokemon newPokemon)
    {
        //Checks if the player has space (Currently not necessary).
        if (team.Count < 6) 
        {
            //Adds the Pokémon to the team list.
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
            //Not necessary to call but there for the sake of it. Useful if the box was implemented and Pokémon could be caught.
            UI.ShowText($"Your party is full! {newPokemon.name} has been send to your box");
        }
    }

    public Pokemon GetFirstAvailablePokemon()
    {
        //Checks the list of Pokémon in the team list and takes the first available one that has above 0 health.
        //Useful if the player could have a team of 6 where swapping away from dead Pokémon was necessary. 
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
