using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.Instance.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            HealthBar.Instance.SetHealth(currentHealth);
            this.enabled = false;
            GetComponent<CharacterController2D>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Hurt");
            HealthBar.Instance.SetHealth(currentHealth);
        }
    }
}
