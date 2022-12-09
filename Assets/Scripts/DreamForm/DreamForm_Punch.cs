using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamForm_Punch : MonoBehaviour
{

    // public Animator DreamAttack_anim

    public Transform attackPoint;
    public float attackRange = 1;
    public LayerMask enemyLayers; // detecting enemies

    public float attackDamage;

    [SerializeField] private Animator _anim;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Checks the left mouse button
        {
            Attack();
        }

    }

    void Attack()
    {

        Debug.Log("You pressed mouse 1");

        // insert attack animation
        _anim.SetTrigger("Attack");

        //detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {

            Debug.Log("We Hit" + enemy.name);

            enemy.GetComponent<HitPoints>().TakeDamage(attackDamage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
