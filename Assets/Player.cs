using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;

    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            healthBar.SetHealth(currentHealth);
            this.enabled = false;
            GetComponent<CharacterController2D>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Hurt");
            healthBar.SetHealth(currentHealth);
        }
    }
}
