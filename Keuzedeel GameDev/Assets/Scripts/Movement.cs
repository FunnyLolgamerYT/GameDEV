using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float horizontal;
    private float speed =16f;
    private float jumpingPower = 24f;
    private bool isFacingRight = true;
    private float acceleration = 10f;
    private float velocity = 0;
    private float maxVelocity = 100;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask groundLayer;
    // Update is called once per frame
    void Update()
    {
        //Constant updating if A / D are being pressed
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
        //constantly checking if the character model needs to be flipped
        Flip();
    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && velocity <= maxVelocity)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }
}
