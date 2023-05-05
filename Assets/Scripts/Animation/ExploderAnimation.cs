using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderAnimation : MonoBehaviour
{
    [SerializeField] private EnemyExplode E_E;
    [SerializeField] private EnemyPatrol E_P;
    [SerializeField] private EnemyMoveToPlayer E_MT;
    [SerializeField] private ExploderController E_EC;
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
    }
    private void WalkAnimation()
    {
        if (E_P.enabled || E_MT.enabled && E_MT._Distance != 0)
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
        if (E_E.enabled)
        {
            if (E_E.IsCharging)
            {
                _anim.SetBool("IsCharge", true);
            }
            if (E_E.IsAttacking)
            {
                _anim.SetBool("IsCharge", false);
                _anim.SetBool("IsAttack", true);
            }
            if (!E_E.IsAttacking)
            {
                _anim.SetBool("IsAttack", false);
            }
        }

    }
    private void HurtAnimation()
    {
        if (HP._IsTakingDamage)
        {
            if(!E_E.IsCharging && !E_E.IsAttacking && !E_E.IsAttacking)
            {
                StartCoroutine(Hurt());
            }

        }
    }
    private IEnumerator Hurt()
    {
        _anim.SetBool("IsTakingDamage", true);
        yield return new WaitForSeconds(0.25f);
        _anim.SetBool("IsTakingDamage", false);
        HP._IsTakingDamage = false;
    }
    private void Die()  //Use in animation
    {
        Destroy(gameObject);
    }
}
