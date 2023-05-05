using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelderAnimation : MonoBehaviour
{
    [SerializeField] private EnemyMeleeAttack E_MA;
    [SerializeField] private EnemyPatrol E_P;
    [SerializeField] private EnemyMoveToPlayer E_MT;
    [SerializeField] private WelderControl E_WC;
    [SerializeField] private HitPoints HP;
    [SerializeField] private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WalkAnimation();

        AttackAnimation();

        HurtAnimation();

        DeadAnimation();
    }

    private void WalkAnimation()
    {
        if(E_P.enabled || E_MT.enabled && E_MT._Distance != 0)
        {
            _anim.SetBool("IsWalking", true);
        }
        else
        {
            _anim.SetBool("IsWalking", false);
        }
    }

    private void AttackAnimation()
    {
        if(E_MA.enabled)
        {
            if (E_MA.IsCharging)
            {
                _anim.SetBool("IsCharge", true);
            }
            if (E_MA.IsAttacking)
            {
                _anim.SetBool("IsCharge", false);
                _anim.SetBool("IsAttack", true);
            }
            if (!E_MA.IsAttacking)
            {
                _anim.SetBool("IsAttack", false);
            }
        }
        
    }
    private void HurtAnimation()
    {
        if (HP._IsTakingDamage && !E_WC._IsDead)
        {
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        _anim.SetBool("IsTakingDamage", true);
        yield return new WaitForSeconds(0.25f);
        _anim.SetBool("IsTakingDamage", false);
        HP._IsTakingDamage = false;
    }

    private void DeadAnimation()
    {
        if(E_WC._IsDead)
        {
            _anim.SetTrigger("Die");
        }
    }

    private void Die()  //Use in animation
    {
        Destroy(gameObject);
    }
}
