using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private float drinkHpCooldownTime = 0.5f;
    private float drinkHpNextTime = 0f;
    private KeyCode drinkHpKey = KeyCode.Q;
    
    private Animator animator;
    private Rigidbody2D rb;

    void Update()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown(drinkHpKey) && Time.time > drinkHpNextTime)
        {
            if (InventoryManager.Instance.HasHealthPotions())
            {
                animator.SetTrigger("DrinkPotion");
                InventoryManager.Instance.UseHealthPotion();

                drinkHpNextTime = Time.time + drinkHpCooldownTime;
            }
        }
    }
}
