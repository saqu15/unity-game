using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rollSpeed = 6f;
    public float rollDuration = 0.5f;
    public float rollCooldown = 0.1f;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isRolling = false;
    private bool canRoll = true;
    private int jumpCount = 0;

    private float drinkCooldownTime = 0.5f;
    private float drinkNextTime = 0f;
    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("IsFacingRight", true);
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        Animator animator = GetComponent<Animator>();
        bool isMovingRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool isMovingLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool rollKey = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        if (Input.GetKeyDown(KeyCode.E) && Time.time > drinkNextTime)
        {
            if (InventoryController.Instance.HasHealthPotions())
            {
                animator.SetTrigger("DrinkPotion");
                InventoryController.Instance.UseHealthPotion();

                drinkNextTime = Time.time + drinkCooldownTime;
            }
        }
        if (isRolling)
        {
            rb.velocity = new Vector2(moveX * rollSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }

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
            if (rollKey && !isJumping && canRoll)
            {
                StartCoroutine(PerformRoll());
            }
        }
        else if (isMovingLeft)
        {
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("IsMovingLeft", true);
            if (rollKey && !isJumping && canRoll)
            {
                StartCoroutine(PerformRoll());
            }
        }
        else
        {
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("IsMovingLeft", false);
            isRolling = false;
            animator.SetBool("IsRolling", false);
        }
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            animator.SetBool("IsJumping", true);
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Animator animator = GetComponent<Animator>();
            isJumping = false;
            isRolling = false;
            animator.SetBool("IsRolling", false);
            animator.SetBool("IsJumping", false);
            jumpCount = 0;
        }
    }

    IEnumerator PerformRoll()
    {
        isRolling = true;
        canRoll = false;
        GetComponent<Animator>().SetBool("IsRolling", true);
        yield return new WaitForSeconds(rollDuration);
        isRolling = false;
        GetComponent<Animator>().SetBool("IsRolling", false);
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }
}
