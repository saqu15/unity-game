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
        Animator animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("IsFacingRight", true);
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        Animator animator = GetComponent<Animator>();
        bool isMovingRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool isMovingLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        

        if (moveX > 0)
        {
            animator.SetBool("IsFacingRight", true);
        }
        else if (moveX < 0)
        {
            animator.SetBool("IsFacingRight", false); 
        }
        if (isMovingRight)
        {
            animator.SetBool("IsMovingRight", true);
            animator.SetBool("IsMovingLeft", false);
        }
        else if (isMovingLeft)
        {
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("IsMovingLeft", true);
        }
        else
        {
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("IsMovingLeft", false);
        }
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
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
