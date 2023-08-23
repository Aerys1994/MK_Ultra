using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    public float speed, jumpHeight;
    private float velX, velY;
    Rigidbody2D rb;

    // The following are the variables for the jumping mechanism
    public Transform groundcheck;
    private bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private int jumpsRemaining;
    private bool canJump;
    private float jumpCooldownTimer;
    public float jumpCooldownDuration;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
        jumpCooldownTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if character is grounded to allow jump
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);

        // This mechanism avoids instant jumping once the character touches the ground after a jump
        if (!canJump && isGrounded)
        {
            jumpCooldownTimer -= Time.deltaTime;
            if (jumpCooldownTimer <= 0f)
            {
                canJump = true;
            }
        }

        FlipCharacter();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    public void Movement()
    {
        // Allows the character to move
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;

        rb.velocity = new Vector2(velX * speed, velY);
    }

    public void FlipCharacter()
    {
        // Makes the character change directions with movement
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Jump()
    {
        // Jumping function
        if (isGrounded)
        {
            jumpsRemaining = 2;
        }

        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0 && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumpsRemaining--;
            if (jumpsRemaining == 0) // Establishes the timer for jump allowance
            {
                canJump = false;
                jumpCooldownTimer = jumpCooldownDuration;
            }
        }
    }
}
