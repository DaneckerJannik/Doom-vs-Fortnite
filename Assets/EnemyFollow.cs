using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public float stoppingDistance = 2.0f;
    private Transform target;
    private NavMeshAgent agent;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate = 1f;
    private float fireCount;

    private Animator animator;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Kein Objekt mit dem Tag 'Move' gefunden!");
        }

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.stoppingDistance = stoppingDistance;
    }

    void Update()
{
    if (target != null)
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stoppingDistance)
        {
            if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(target.position);
            }
            SetMovingAnimation(true);  // 💡 Immer sobald Destination gesetzt wird!
        }
        else
        {
            agent.ResetPath();
            SetMovingAnimation(false);
            TryShoot();
        }
    }

    fireCount -= Time.deltaTime;
}


    void SetMovingAnimation(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
    }

    void TryShoot()
    {
        if (fireCount <= 0 && bullet != null && firePoint != null)
        {
            // FirePoint direkt auf den Spieler ausrichten
            firePoint.LookAt(target);

            fireCount = fireRate;

            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Bullet bulletScript = newBullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.shooter = gameObject;
            }

            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }

            Debug.Log("Gegner hat geschossen!");
        }
    }

    void OnDrawGizmos()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(firePoint.position, firePoint.forward * 2f);
        }
    }
}
