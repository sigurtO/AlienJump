using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class Player : MonoBehaviour
{
    private Vector2 moveInput;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private int maxAir = 10;

    [SerializeField]
    private int currentAir = 10;

    [SerializeField]
    public int health = 3; // need to be able to set this in UI

    private bool isGrounded;

    [SerializeField]
    private Image FullHealthBar;
    [SerializeField]
    private ParticleSystem groundEffect;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private UiAirBubble uiAirBubble;

    [SerializeField]
    public bool isHardCore = false;



    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY); // y velocity preservs its grativty (so we keep falling when moving on x axis)
        if (rb.linearVelocity.x > 0)
        {
            _animator.SetBool("isRunning", true);
            sprite.flipX = false;
        }
        else if (rb.linearVelocity.x < 0)
        {
            _animator.SetBool("isRunning", true);
            sprite.flipX = true;
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void Update()
    {
        if (isGrounded && moveInput.y > 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetBool("isJumping", true);
            isGrounded = false;
        }
    }

    private void Start()
    {
        if (GameManager.instance != null)
        {
            isHardCore = GameManager.instance.gameIsHardCore;
        }
        HardcoreCheck();
        UpdateAirBubbleUi();
        StartCoroutine(DecreaseAirOverTime());
    }

    private IEnumerator DecreaseAirOverTime()
    {
        while (currentAir > 0)
        {
            yield return new WaitForSeconds(1f);
            currentAir--;
            UpdateAirBarUI();
        }
        if (currentAir <= 0)
        {
            TakeDamage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        groundEffect.Play();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.5f)
            {
                isGrounded = true;
                _animator.SetBool("isJumping", false);
            }
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   public void RefillAir()
    {
        currentAir = maxAir;
        UpdateAirBarUI();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        RefillAir();
        StartCoroutine(DecreaseAirOverTime()); // Restart air depletion
        UpdateAirBubbleUi();
        if (health <= -1)
        {
            GameOver();
        }
    }

    public void HardcoreCheck()
    {
        if(isHardCore)
        {
            health = 0;
        }
    }    

    void UpdateAirBarUI()
    {
        FullHealthBar.fillAmount = (float)currentAir / maxAir;
    }
    void UpdateAirBubbleUi()
    {
        uiAirBubble.UpdateAirBubbles(health);
    }

    public void ExtraJump()
    {
        isGrounded = true;
    }
}
