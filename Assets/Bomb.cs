using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionDelay = 2f;
    public float explosionRadius = 5f;
    public int damage = 50;
    public GameObject explosionEffect;

    void Start()
    {
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        Debug.Log("💥 Bombe explodiert, trifft " + hits.Length + " Objekte");

        foreach (Collider hit in hits)
        {
            // Schaden an Gegner
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy == null) enemy = hit.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                Debug.Log("➡️ Gegner getroffen: " + hit.name);
                enemy.DamageEnemy(damage);
            }

            // Schaden an Spieler
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player == null) player = hit.GetComponentInParent<PlayerHealth>();
            if (player != null)
            {
                Debug.Log("⚠️ Spieler getroffen von Explosion");
                player.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
