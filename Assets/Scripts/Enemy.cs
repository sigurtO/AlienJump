using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform PosA;
    [SerializeField]
    private Transform PosB;

    private Vector3 PosAStored;
    private Vector3 PosBStored;
    private Vector3 nextPos;

    [SerializeField]
    private float MoveSpeed;

    void Start()
    {
        PosAStored = PosA.position;
        PosBStored = PosB.position;
        nextPos = PosAStored;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, MoveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, nextPos) < 0.01f)
        {
            if (nextPos == PosAStored)
            {
                nextPos = PosBStored;
            }
            else
            {
                nextPos = PosAStored;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(1);
        }
    }
}
