using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public float lifeTime = 3f;
    public Rigidbody theRigidbody;
    public int damage = 1;
    public bool damageEnemy = true, damagePlayer = false;

    [HideInInspector]
    public GameObject shooter;

    void Start()
    {
        Debug.Log("Bullet fired");

        if (theRigidbody == null)
        {
            theRigidbody = GetComponent<Rigidbody>();
        }

        theRigidbody.linearVelocity = transform.forward * bulletSpeed;

        var trail = GetComponent<TrailRenderer>();
        if (trail != null) trail.Clear();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shooter)
        {
            Debug.Log("Bullet trifft eigenen Schützen – wird ignoriert.");
            return;
        }

        Debug.Log("Bullet kollidiert mit: " + other.name);

        // 🧨 Schaden an Gegner
        if (damageEnemy)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth == null)
                enemyHealth = other.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null)
            {
                Debug.Log("➡️ Gegner getroffen, Schaden: " + damage);
                enemyHealth.DamageEnemy(damage);
                Destroy(gameObject);
                return;
            }
        }

        // 💥 Schaden an Spieler
        if (damagePlayer && other.CompareTag("Player"))
        {
            Debug.Log("⚠️ Spieler wurde getroffen");

            // 🩸 Schaden zufügen
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth == null)
                playerHealth = other.GetComponentInParent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("➡️ Schaden am Spieler: " + damage);
            }
            else
            {
                Debug.LogWarning("⚠️ Kein PlayerHealth-Skript gefunden am getroffenen Objekt!");
            }

            // 📷 Kamera-Shake
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                PlayerCameraShake shake = mainCam.GetComponent<PlayerCameraShake>();
                if (shake != null)
                {
                    shake.Shake();
                }
            }

            // 🔴 Flash bei Treffer
            DamageFlash flash = FindObjectOfType<DamageFlash>();
            if (flash != null)
            {
                flash.TriggerFlash();
            }

            Destroy(gameObject);
            return;
        }

        // Alles andere: zerstören
        if (!other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
