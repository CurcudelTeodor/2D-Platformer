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


    private float directionX = 0f;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum AnimationState { idle, running, jumping, falling }

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
        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        //velocity.y going left = -6.854534E-06 (for movement speed = 7)
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
