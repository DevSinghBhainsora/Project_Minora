using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    public PlayerCombat PlayerCombat;

    private bool doubleJump;
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private enum MovementState { idle, running, jumping, falling }


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                doubleJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            else if (doubleJump)
            {
                doubleJump = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpeedBoost"))
        {
            Destroy(collision.gameObject);
            Debug.Log("PowerUp Speed");
            moveSpeed = 18f;
            sprite.color = Color.red;
            StartCoroutine(ResetPower());
        }
        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            Destroy(collision.gameObject);
            Debug.Log("PowerUp Jump");
            jumpForce = 20f;
            sprite.color = Color.cyan;
            StartCoroutine(ResetPower());
        }
        if (collision.gameObject.CompareTag("DamageBoost"))
        {
            Destroy(collision.gameObject);
            Debug.Log("PowerUp Empress");
            PlayerCombat.attackDamage = 80;
            sprite.color = Color.yellow;
        }

    }
    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(6);
        moveSpeed = 8f;
        jumpForce = 6.5f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}