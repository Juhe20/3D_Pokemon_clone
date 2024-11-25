using UnityEngine;

public class PikachuBehaviour : MonoBehaviour
{
    public Transform player;
    private float outOfRange = 2f;
    private float speed = 5f;
    public float teleportDistance = 10f;
    public Collider playerCollider;  // Player's collider
    public Collider pikachuCollider;  // Pikachu's collider

    private void Start()
    {
        // Ignore collisions between the player's and Pikachu's colliders
        Physics.IgnoreCollision(playerCollider, pikachuCollider);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        transform.LookAt(player);

        // Move Pikachu towards the player if it's too far
        if (distanceToPlayer > outOfRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        // Teleport Pikachu if the distance exceeds the teleport distance
        if (distanceToPlayer > teleportDistance)
        {
            TeleportBehindPlayer();
        }
    }

    private void TeleportBehindPlayer()
    {
        Vector3 teleportPosition = player.position - player.forward * outOfRange;
        transform.position = teleportPosition;
    }
}
