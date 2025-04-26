using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator fehlt am Gegner!");
        }
    }

    public void DamageEnemy(int damage)
    {
        if (isDead) return;

        health -= damage;
        Debug.Log("Gegner getroffen! HP: " + health);

        if (health > 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Hit");
            }
        }
        else
        {
            Die();
        }
    }

    void Die()
{
    isDead = true;
    Debug.Log("Gegner gestorben");

    if (animator != null)
        animator.SetTrigger("Die");

    NavMeshAgent agent = GetComponent<NavMeshAgent>();
    if (agent != null)
        agent.enabled = false;

    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null)
        rb.isKinematic = true; // verhindert Physikexplosionen

    // Gegnerverhalten abschalten
    MonoBehaviour followScript = GetComponent<MonoBehaviour>(); // EnemyFollow
    if (followScript != null) followScript.enabled = false;

    Destroy(gameObject, 3f);
}

}
