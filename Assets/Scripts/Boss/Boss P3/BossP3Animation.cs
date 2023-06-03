using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossP3Animation : MonoBehaviour
{
    [SerializeField] private GameObject prop;
    [SerializeField] private GameObject fireball;

    [SerializeField] private BossMeleeAttack B_MeleeAttack;
    [SerializeField] private BossSummonLaser B_LaserSummon;
    [SerializeField] private BossHP_P3 B_HP;

    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _animHand;
    [SerializeField] private Animator _animCore;

    private float HPAfterTakeDamage;
    private float HPBeforeTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        HPAfterTakeDamage = B_HP.currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        HPBeforeTakeDamage = B_HP.currentHP;

        MeleeAttack();
        HurtAnimation();
        FallAnimation();
        BossCore();
    }

    private void BossCore()
    {
        if(B_HP.isStunned)
        {
            _animCore.SetBool("IsShow",true);
        }
    }

    private void MeleeAttack()
    {
        if (B_MeleeAttack.IsCharging)
        {
            _anim.SetBool("IsCharge", true);
            //prop.SetActive(false);
        }
        if (B_MeleeAttack.IsAttacking)
        {
            _anim.SetBool("IsCharge", false);
            _anim.SetBool("IsAttack", true);
        }
        if (!B_MeleeAttack.IsAttacking)
        {
            _anim.SetBool("IsAttack", false);
            //prop.SetActive(true);
        }
    }

    private void FallAnimation()
    {
        if(B_HP.isStunned)
        {
            _anim.SetBool("IsFall", true);
        }
    }

    private void HurtAnimation()
    {
        if (HPBeforeTakeDamage != HPAfterTakeDamage)
        {
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        _anim.SetBool("IsTakingDamage", true);
        _animHand.SetBool("IsTakingDamage", true);
        fireball.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        _anim.SetBool("IsTakingDamage", false);
        _animHand.SetBool("IsTakingDamage", false);
        fireball.SetActive(true);
        HPAfterTakeDamage = HPBeforeTakeDamage;
    }

    public void ActiveProp()
    {
        prop.SetActive(true);
        //Debug.Log("Active Prop");
    }

    public void DisableProp()
    {
        prop.SetActive(false);
        //Debug.Log("Dis Prop");
    }
}
