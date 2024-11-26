using System.Collections;
using UnityEngine;

public class CheckFlag : MonoBehaviour
{
    public Transform player;
    public UIController UI;
    public CharacterMovement movement;
    public Animator playerAnimator;
    private bool isTextDisplayed = false;
    public PokeballFlagCheck flagCheck;

    private void OnTriggerEnter(Collider other)
    {
        //Checks bool to see if the invisible wall should be active or not.
        if (flagCheck.isPikachuRecieved == true) return;
        //If the player hasn't picked up Pikachu yet the UI will display a message and move the player back slightly.
        if (other.CompareTag("Player") && !isTextDisplayed)
        {
            movement.inEvent = true;
            UI.ShowText("I should probably visit Professor Oak before I venture into the wild");
            isTextDisplayed = true;
            //Calculates a distance the player should walk backwards.
            Vector3 targetPosition = player.transform.position - player.transform.forward * 3f;
            StartCoroutine(MovePlayerBack(targetPosition));
            playerAnimator.SetBool("isRunning", false);
        }
    }

    private void Update()
    {
        if (flagCheck.isPikachuRecieved == true) return;
        if (isTextDisplayed && Input.GetKeyDown(KeyCode.Space))
        {
            UI.HideText();
            movement.inEvent = false;
            isTextDisplayed = false;
        }
    }

    //Coroutine to make the player moving back more smooth instead of teleporting them.
    private IEnumerator MovePlayerBack(Vector3 targetPosition)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPosition = player.position;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            player.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }
        player.position = targetPosition;
    }

}
