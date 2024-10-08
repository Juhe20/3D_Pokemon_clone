using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Encounter : MonoBehaviour
{
    public CharacterMovement character;
    private bool isEncountered = false;
    private Vector3 playerPosition;
    private void OnTriggerStay(Collider other)
    {
        if (character.isRunning == true)
        {
            if (Random.Range(1, 100) == 1)
            {
                playerPosition = character.Player.transform.position; 
                isEncountered = true;
                SceneManager.LoadScene("BattleScene");
            }
            else
            {
                isEncountered = false;
            }
        }
        Debug.Log(isEncountered);
    }
}
