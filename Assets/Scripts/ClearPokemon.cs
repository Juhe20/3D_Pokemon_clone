using UnityEngine;

public class ClearPokemon : MonoBehaviour
{
    //Function to clear all Pokémon in the PokemonParent GameObject.
    //Used to clear out the battle area after ending a battle.
    public void ClearAllPokemon()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
