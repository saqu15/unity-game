using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public PolygonCollider2D attackCollider1;
    public PolygonCollider2D attackCollider2;
    public PolygonCollider2D attackCollider3;
    public BoxCollider2D playerCollider;
    
    public int maxHealth = 100;
    public int enemyDamage = 20;
    int currentHealth;
    int damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = GameObject.Find("Player").GetComponent<PlayerCombat>().attackDamage;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void DealDamage()
    {
        GameObject.Find("Player").GetComponent<Player>().TakeDamage(enemyDamage);
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider == attackCollider1)
        {
            TakeDamage(damage / 2);
        }
        else if (col.collider == attackCollider2)
        {
            TakeDamage(damage);
        }
        else if (col.collider == attackCollider3)
        {
            TakeDamage(damage);
        }
        else if (col.collider == playerCollider)
        {
            DealDamage();
        }

    }
}
