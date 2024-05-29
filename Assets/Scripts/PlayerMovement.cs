using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask jumpableGround;

    private int jumpCount = 0;
    private const int maxJumpCount = 1;

    private float directionX = 0f;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float jumpForce = 14f;

    private enum AnimationState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>(); 
    }

    // Update is called once per frame
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
           
        // sprinting
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            rb.velocity = new Vector2(directionX * sprintSpeed, rb.velocity.y);
            anim.speed = 1.5f;
            jumpForce = 16f;
        }
        else
        {
            rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
            anim.speed = 1.0f;
            jumpForce = 14f;
        }


        if (Input.GetButtonDown("Jump") && (IsGrounded() || jumpCount < maxJumpCount))
        {   
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }

        if (IsGrounded())
        {
            jumpCount = 0;
        }

        UpdateAnimationState();
        // Debug.Log(rb.velocity.y);
    }

    private void UpdateAnimationState()
    {
        AnimationState state;

        // moving right
        if (directionX > 0f)
        {
            state = AnimationState.running;
            sprite.flipX = false;
        }
        // moving left
        else if (directionX < 0f)
        {
            state = AnimationState.running;
            sprite.flipX = true;
        }
        else
        {
            state = AnimationState.idle;
        }

        // velocity.y going right = 6.854534E-06
        // velocity.y going left = -6.854534E-06 (for movement speed = 7)
        if (rb.velocity.y > .1f)
        {
            state = AnimationState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = AnimationState.falling;
        }


        anim.SetInteger("state", (int)state);
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
