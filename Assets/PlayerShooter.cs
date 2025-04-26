using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    public float bulletSpeed = 50f;
    public float bulletLifeTime = 3f;

    private float fireCooldown;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") && fireCooldown <= 0f)
        {
            fireCooldown = fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.lifeTime = bulletLifeTime;
            bulletScript.damageEnemy = true;
            bulletScript.damage = 25; // oder wie viel Schaden du machen willst
        }

        Debug.Log("Spieler hat geschossen!");
    }
}
