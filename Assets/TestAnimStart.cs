using UnityEngine;

public class TestAnimStart : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Attack"); // Name deines States im Animator
    }
}
