using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour
{
    public Transform player;
    public float visionRange = 20f;
    public float visionAngle = 60f;
    public float shootCooldown = 2f;
    public int damage = 1;
    public LayerMask obstacleMask;

    private float lastShootTime;
    private Animator animator;
    private bool isShooting = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator fehlt am Gegner!");
        }
    }

    void Update()
    {
        if (player == null || isShooting)
            return;

        LookAtPlayer();

        if (CanSeePlayer() && Time.time > lastShootTime + shootCooldown)
        {
            if (IsLookingAtPlayer())
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    void LookAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2f);
    }

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (angle < visionAngle / 2f && Vector3.Distance(transform.position, player.position) <= visionRange)
        {
            if (!Physics.Raycast(transform.position + Vector3.up, dirToPlayer, Vector3.Distance(transform.position, player.position), obstacleMask))
            {
                return true;
            }
        }

        return false;
    }

    bool IsLookingAtPlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, dirToPlayer);
        return dot > 0.95f;
    }

    void Shoot()
    {
        isShooting = true;

        if (animator != null)
        {
            animator.SetBool("isShooting", true);
            animator.SetTrigger("Shoot");
        }

        Debug.Log("Gegner schießt!");

        Ray ray = new Ray(transform.position + Vector3.up, (player.position - transform.position).normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, visionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Target targetScript = hit.collider.GetComponent<Target>();
                if (targetScript != null)
                {
                    targetScript.TakeDamage(damage);
                    Debug.Log("Spieler getroffen!");
                }
            }
        }

        StartCoroutine(ResetShootState());
    }

    IEnumerator ResetShootState()
    {
        yield return new WaitForSeconds(1f); // Passe dies an die Dauer deiner Shoot-Animation an

        isShooting = false;
        if (animator != null)
        {
            animator.SetBool("isShooting", false);
        }
    }

    // Optional: Sichtfeld anzeigen im Editor
    void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up, player.position);

        Vector3 rightLimit = Quaternion.Euler(0, visionAngle / 2f, 0) * transform.forward;
        Vector3 leftLimit = Quaternion.Euler(0, -visionAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up, rightLimit * visionRange);
        Gizmos.DrawRay(transform.position + Vector3.up, leftLimit * visionRange);
    }
}
