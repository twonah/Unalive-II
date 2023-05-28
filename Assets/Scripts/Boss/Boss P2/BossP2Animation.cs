using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossP2Animation : MonoBehaviour
{
    [SerializeField] private BossCoreReview B_CoreReview;
    [SerializeField] private BossControlP2 B_Control2;
    [SerializeField] private BossMeleeAttack B_MeleeAttack;
    [SerializeField] private BossSummonLaser B_LaserSummon;
    [SerializeField] private HitPoints HP;
    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _animHand;
    [SerializeField] private Animator _animHandFire;

    [SerializeField] private GameObject prop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FallAnimation();
        CallAnimation();
        AttackAnimation();
        HurtAnimation();
    }
    private void FallAnimation()
    {
        if (HP._CurrentHitPoints <= 0 /*|| B_Control2.IsStun*/)
        {
            _anim.SetBool("IsFall", true);
            //prop.SetActive(false);
        }
        else
        {
            if (!B_CoreReview.IsCoreOut)
            {
                _anim.SetBool("IsFall", false);
            }

        }

    }

    private void CallAnimation()
    {
        if (B_LaserSummon._IsSummonLaser)
        {
            _anim.SetBool("IsCall", true);
        }
        else if(!B_LaserSummon._IsSummonLaser)
        {
            _anim.SetBool("IsCall", false);
        }
    }

    private void AttackAnimation()
    {
        if (B_MeleeAttack.enabled)
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

    }
    private void HurtAnimation()
    {
        if (HP._IsTakingDamage && HP._CurrentHitPoints > 0)
        {
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        _anim.SetBool("IsTakingDamage", true);
        _animHand.SetBool("IsTakingDamage", true);
        _animHandFire.SetBool("IsTakingDamage", true);
        yield return new WaitForSeconds(0.25f);
        _anim.SetBool("IsTakingDamage", false);
        _animHand.SetBool("IsTakingDamage", false);
        _animHandFire.SetBool("IsTakingDamage", false);
        HP._IsTakingDamage = false;
    }

    private void DisableBoss()  //Use in animation
    {
        gameObject.SetActive(false);
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
