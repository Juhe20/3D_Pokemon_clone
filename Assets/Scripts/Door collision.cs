using UnityEngine;
using Cinemachine;

public class Doorcollision : MonoBehaviour
{
    public Transform teleportPoint;
    public Transform player;
    public CinemachineFreeLook camera;

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the player triggered the collider. Temporarily disable movement from the player to teleport them.
        //If not disabled the teleport doesn't work. The player's character controller stops the movement if they still have control over it.
        if (other.CompareTag("Player"))
        {
            var movementScript = other.GetComponent<CharacterController>();
            movementScript.enabled = false;
            TeleportPlayer();
            movementScript.enabled = true;
        }
    }

    public void TeleportPlayer()
    {
        //Make the player go to a teleportPoint marked in the inspector with a GameObject. Makes the camera teleport smoothly with it.
        Vector3 position = teleportPoint.position - player.position;
        player.position = teleportPoint.position;
        camera.OnTargetObjectWarped(player, position);
    }
}
