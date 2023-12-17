using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;

    void Start()
    {
        HealthBar.Instance.SetMaxHealth(maxHealth);
        HealthBar.Instance.SetHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        int currentHealth = (int)HealthBar.Instance.GetHealth();
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
