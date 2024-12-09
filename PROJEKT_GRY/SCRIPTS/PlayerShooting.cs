using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint; 
    public float bulletSpeed = 10f;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (playerHealth != null && playerHealth.tripleShotActive)
        {
            ShootBullet(0);
            ShootBullet(45);
            ShootBullet(-45);
        }
        else
        {
            ShootBullet(0);
        }
    }

    void ShootBullet(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
        rb.linearVelocity = direction * bulletSpeed;
    }
}
