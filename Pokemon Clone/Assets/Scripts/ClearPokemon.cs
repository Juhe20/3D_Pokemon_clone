using UnityEngine;

public class ClearPokemon : MonoBehaviour
{
    public void ClearAllPokemon()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
