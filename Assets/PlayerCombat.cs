using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("NextAttack");
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }
    public void SetAttackDamage(int newDamage)
    {
        attackDamage = newDamage;
    }
}
