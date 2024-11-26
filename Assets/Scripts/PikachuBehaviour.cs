using UnityEngine;

public class PikachuBehaviour : MonoBehaviour
{
    public Transform player;
    private float outOfRange = 2f;
    private float speed = 5f;
    public float teleportDistance = 10f;
    public Collider playerCollider;
    public Collider pikachuCollider;

    private void Start()
    {
        //Ignores collision between the player and Pikachu since it will be following the player at all times outside battle.
        Physics.IgnoreCollision(playerCollider, pikachuCollider);
    }

    void Update()
    {
        //Calculates distance between Pikachu and the player and makes it look at the player.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        transform.LookAt(player);

        //Check to see when it's out of range so it can start following again.
        if (distanceToPlayer > outOfRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        //Teleport in case Pikachu gets stuck and left behind. Also used when the player is teleported back outside the lab after acquiring Pikachu.
        if (distanceToPlayer > teleportDistance)
        {
            TeleportBehindPlayer();
        }
    }

    private void TeleportBehindPlayer()
    {
        //Calculates a small distance behind the player to teleport to if completely out of range.
        Vector3 teleportPosition = player.position - player.forward * outOfRange;
        transform.position = teleportPosition;
    }
}
