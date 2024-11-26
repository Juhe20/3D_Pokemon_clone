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
            movementScript.enabled = false;
            TeleportPlayer();
            movementScript.enabled = true;
        }
    }

    public void TeleportPlayer()
    {
        Vector3 position = teleportPoint.position - player.position;
        player.position = teleportPoint.position;
        camera.OnTargetObjectWarped(player, position);
    }
}
