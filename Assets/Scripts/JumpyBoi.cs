using UnityEngine;

public class JumpyBoi : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight = 15f;

    [SerializeField]
    private Animator animator;



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                animator.SetTrigger("JumpedOn");
            }
        }
    }


}
