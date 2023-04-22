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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) // Checks the left mouse button
        {
            StartCoroutine(attackDelay());
        }

    }

    void Attack()
    {

        Debug.Log("You pressed mouse 1");

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

    IEnumerator attackDelay()
    {
        yield return new WaitForSeconds(1f);
        Attack();
    }
}
