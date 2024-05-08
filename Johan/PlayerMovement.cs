using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    private Rigidbody2D rb;
    private new BoxCollider2D collider;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private GameObject archTarget;
    [SerializeField] private GameObject firePoint;


    //Layer Masks
    [SerializeField] private LayerMask jumpGround;

    //Movement parameters
    static public float moveX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    static public bool facingRight = true;

    //Arch position
    private float archObjectDistanceX = 1.8f;
    private float archObjectDistanceY = 1.25f;

    private float firePointDistanceX = 0.3f;
    private float firePointDistanceY = 1f;

    //Coyote Time
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private enum MovementState { idle, running, jumping, falling }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (moveX < 0f)
        {
            facingRight = false;
            sprite.flipX = true;
        }
        else if (moveX > 0f)
        {
            facingRight = true;
            sprite.flipX = false;
        }

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            coyoteTimeCounter = 0f;
        }

        UpdateAnimationState();

        UpdateFirePoint();
        UpdateArch();
        //Flip(transform);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (moveX > 0f)
        {
            state = MovementState.running;
        }
        else if (moveX < 0f)
        {
            state = MovementState.running;
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
    private void Flip(Transform parent)
    {
        if (moveX < 0f)
        {
            facingRight = false;
        }
        else if (moveX > 0f)
        {
            facingRight = true;
        }

        switch(facingRight)
        {
            case true:
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                foreach (Transform child in parent)
                {
                    child.rotation = Quaternion.Euler(0f, 0f, 0f);
                    Flip(child);
                }
                return;
            case false:
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                foreach (Transform child in parent)
                {
                    child.rotation = Quaternion.Euler(0f, 180f, 0f);
                    Flip(child);
                }
                return;
        }
        
    }
    private void UpdateArch()
    {
        switch(facingRight)
        {
            case true:
                archTarget.transform.position = new Vector2(rb.position.x + archObjectDistanceX, rb.position.y + archObjectDistanceY);
                return;
            case false:
                archTarget.transform.position = new Vector2(rb.position.x - archObjectDistanceX, rb.position.y + archObjectDistanceY);
                return;
        }
        
    }
    private void UpdateFirePoint()
    {
        switch (facingRight)
        {
            case true:
                firePoint.transform.position = new Vector2(rb.position.x + firePointDistanceX, rb.position.y + firePointDistanceY);
                return;
            case false:
                firePoint.transform.position = new Vector2(rb.position.x - firePointDistanceX, rb.position.y + firePointDistanceY);
                return;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .4f, jumpGround);
    }
}
