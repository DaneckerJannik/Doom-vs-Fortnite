using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float shootingRange = 100f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject impactEffect;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Linksklick / Ctrl
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange))
        {
            Debug.Log("Raycast getroffen: " + hit.collider.name);

            if (impactEffect != null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            // Sucht Target-Script auch bei Parent-Objekten (z. B. wenn Collider am Mesh hängt)
            Target target = hit.collider.GetComponentInParent<Target>();
            if (target != null)
            {
                Debug.Log("Target gefunden – Schaden wird zugefügt.");
                target.TakeDamage(25);
            }
            else
            {
                Debug.Log("Kein Target-Script gefunden bei: " + hit.collider.name);
            }
        }
        else
        {
            Debug.Log("Raycast hat NICHTS getroffen.");
        }
    }
}
