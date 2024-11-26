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
        Physics.IgnoreCollision(playerCollider, pikachuCollider);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        transform.LookAt(player);
        if (distanceToPlayer > outOfRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
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
