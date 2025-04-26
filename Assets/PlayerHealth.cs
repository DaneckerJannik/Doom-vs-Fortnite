using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Transform cameraTransform; // Z.B. deine First Person Kamera
    public MonoBehaviour[] componentsToDisable; // Bewegung, Kamera, Schießen, etc.
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("🩸 Schaden: " + amount + " | HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("☠️ Spieler ist tot!");

        // Kamera nach unten kippen
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(80f, 0f, 0f);
        }

        // Bewegung etc. deaktivieren
        foreach (var comp in componentsToDisable)
        {
            comp.enabled = false;
        }

        // Optional: Cursor anzeigen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Optional: Crosshair deaktivieren
        GameObject crosshair = GameObject.Find("Crosshair");
        if (crosshair != null)
        {
            crosshair.SetActive(false);
        }
    }
}
