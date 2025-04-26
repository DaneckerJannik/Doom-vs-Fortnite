using UnityEngine;
using StarterAssets;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 20f;

    private void OnTriggerEnter(Collider other)
{
    FirstPersonController fpc = other.GetComponent<FirstPersonController>();
    if (fpc != null)
    {
        fpc._verticalVelocity = Mathf.Sqrt(bounceForce * -2f * fpc.Gravity);
    }
}

}
