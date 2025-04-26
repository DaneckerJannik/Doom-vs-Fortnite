using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform throwPoint;
    public float throwForce = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowBomb();
        }
    }

    void ThrowBomb()
{
    if (bombPrefab == null)
    {
        Debug.LogWarning("⚠️ Kein Bomben-Prefab zugewiesen!");
        return;
    }

    GameObject bomb = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);
    Rigidbody rb = bomb.GetComponent<Rigidbody>();

    if (rb != null)
    {
        Vector3 throwDirection = throwPoint.forward * throwForce + throwPoint.up * (throwForce / 2f); // Vorwärts + leicht nach oben
        rb.AddForce(throwDirection, ForceMode.VelocityChange);
    }
    else
    {
        Debug.LogWarning("⚠️ Keine Rigidbody-Komponente an der Bombe gefunden!");
    }
}

}
