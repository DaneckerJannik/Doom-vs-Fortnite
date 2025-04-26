using UnityEngine;
using UnityEngine.UI;

public class DeathScreenFade : MonoBehaviour
{
    public Image deathOverlay;
    public float fadeSpeed = 1f;
    private bool isDead = false;

    void Start()
    {
        if (deathOverlay != null)
        {
            Color c = deathOverlay.color;
            c.a = 0f;
            deathOverlay.color = c;
        }
    }

    void Update()
    {
        if (isDead && deathOverlay != null)
        {
            Color c = deathOverlay.color;
            c.a = Mathf.MoveTowards(c.a, 1f, fadeSpeed * Time.deltaTime);
            deathOverlay.color = c;
        }
    }

    public void TriggerDeathFade()
    {
        isDead = true;
    }
}
