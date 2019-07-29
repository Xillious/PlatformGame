using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float moveInput;

    public float jumpForce;              //the amount the player jumps  
    private bool jumping;                // is the player currently jumping
    public float jumpTimeCounter;
    public float jumpTime;               // how long can a plyer jump until they start to fall

    public bool isGrounded;             // is the player on the ground
    public Transform groundCheck; 
    public LayerMask whatIsGround;       // what is considered ground
    const float groundedRadius = .2f;    // circle at the players feet to check for ground

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

        if (isGrounded == true)
        {
            //jumping = false;
            jumpTimeCounter = jumpTime;
        }

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            
            rb.velocity = Vector2.up * jumpForce;
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.Space) && jumping == true)
        {
            if (jumpTimeCounter >= 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumping = false;
            }
        }
    }


}
