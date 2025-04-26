using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    public float flashDuration = 0.2f;
    public Color flashColor = new Color(1, 0, 0, 0.4f); // halbtransparentes Rot

    private Image damageImage;
    private float currentTime = 0f;

    void Start()
    {
        damageImage = GetComponent<Image>();
        damageImage.color = new Color(1, 0, 0, 0f); // Start: unsichtbar
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            float alpha = Mathf.Lerp(0, flashColor.a, currentTime / flashDuration);
            damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
        }
        else
        {
            damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
        }
    }

    public void TriggerFlash()
    {
        currentTime = flashDuration;
        damageImage.color = flashColor;
    }
}
