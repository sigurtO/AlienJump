using UnityEngine;

public class AirBubbleScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.RefillAir();
                Destroy(gameObject);
            }
        }
    }
}
