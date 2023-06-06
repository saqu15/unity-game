using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;        
    public float jumpForce = 5f;        

    private Rigidbody2D rb;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        float speed = Mathf.Abs(moveX);
        
        Animator animator = GetComponent<Animator>();
        if (moveX > 0)
        {
            animator.SetBool("IsFacingRight", true); 
        }
        else if (moveX < 0)
        {
            animator.SetBool("IsFacingRight", false);
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
        animator.SetFloat("Speed", speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Animator animator = GetComponent<Animator>();
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }
}
