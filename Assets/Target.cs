using UnityEngine;

public class Target : MonoBehaviour
{
    public int health = 10;

    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator wurde nicht gefunden! Bitte sicherstellen, dass der Animator am Objekt hängt.");
        }
        else
        {
            Debug.Log("Animator korrekt zugewiesen.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            Debug.Log("Ziel ist bereits tot, kein weiterer Schaden.");
            return;
        }

        health -= damage;
        Debug.Log("Target getroffen! Aktuelle Health: " + health);

        if (animator != null)
        {
            Debug.Log("Trigger 'Hit' wird gesetzt.");
            animator.SetTrigger("Hit");
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Target gestorben!");

        if (animator != null)
        {
            Debug.Log("Trigger 'Die' wird gesetzt.");
            animator.SetTrigger("Die");
        }

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        // Manueller Test für Animationen
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H gedrückt – Hit-Animation ausgelöst.");
            if (animator != null)
            {
                animator.SetTrigger("Hit");
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K gedrückt – Die-Animation ausgelöst.");
            if (animator != null)
            {
                animator.SetTrigger("Die");
            }
        }
    }
}
