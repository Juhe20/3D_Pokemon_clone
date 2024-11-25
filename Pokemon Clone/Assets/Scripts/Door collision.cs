using UnityEngine;
using Cinemachine;

public class Doorcollision : MonoBehaviour
{
    public Transform teleportPoint;
    public Transform player;
    public CinemachineFreeLook camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var movementScript = other.GetComponent<CharacterController>();
            if (movementScript != null)
                movementScript.enabled = false;

            TeleportPlayer();

            if (movementScript != null)
                movementScript.enabled = true;
        }
    }

    public void TeleportPlayer()
    {
        if (camera == null || teleportPoint == null || player == null)
            return;

        // Calculate the position delta for the warp
        Vector3 positionDelta = teleportPoint.position - player.position;

        // Teleport the player
        player.position = teleportPoint.position;

        // Notify Cinemachine about the warp
        camera.OnTargetObjectWarped(player, positionDelta);
    }
}
