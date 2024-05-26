using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Transform player;
    public float fireCooldown = 1f;
    private float fireCooldownTimer = 0f;

    void Update()
    {
        fireCooldownTimer -= Time.deltaTime;
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;   // °Å¸®

        if (distanceToPlayer <= 5f && IsPlayerInSector(directionToPlayer))
        {
            if (fireCooldownTimer <= 0f)
            {
                transform.rotation = Quaternion.LookRotation(directionToPlayer);
                Fire();
                fireCooldownTimer = fireCooldown;
            }
        }
        else
        {
            transform.Rotate(Vector3.up, 50f * Time.deltaTime);
        }
    }

    private bool IsPlayerInSector(Vector3 directionToPlayer)
    {
        Vector3 forward = new Vector3(0, 0, 1);
        directionToPlayer.Normalize();

        float dotProduct = Vector3.Dot(forward, directionToPlayer);
        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

        return angle <= 30f;
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
