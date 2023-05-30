using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerAnimation : MonoBehaviour
{
    [SerializeField] private EnemyShoot E_S;
    [SerializeField] private EnemyPatrol E_P;
    [SerializeField] private EnemyMoveToPlayer E_MT;
    [SerializeField] private GunnerControl E_GC;
    [SerializeField] private HitPoints HP;
    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _shootAnim;
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
        if (E_P.enabled || E_MT.enabled && E_MT._MoveToSpeed != 0)
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
        if (E_S.isShoot)
        {
            _anim.SetBool("IsAttack", true);
            _shootAnim.SetBool("EffectStart", true);
        }
        else
        {
            _anim.SetBool("IsAttack", false);
            _shootAnim.SetBool("EffectStart", false);
        }

    }
    private void HurtAnimation()
    {
        if (HP._IsTakingDamage && !E_GC._IsDead)
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
        if (E_GC._IsDead)
        {
            _anim.SetTrigger("Die");
        }
    }

    private void Die()  //Use in animation
    {
        Destroy(gameObject);
    }

    public void GunnerEffect()
    {
        _shootAnim.SetBool("EffectStart", false);
    }
}
