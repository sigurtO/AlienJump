using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    [SerializeField]
   private float gravity = 1;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.gravityScale = gravity;
            animator.SetTrigger("buttonDown");
        }
    }
}
