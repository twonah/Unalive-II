using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    [SerializeField] private BossCoreReview B_CoreReview;
    [SerializeField] private BossCoreReviewArea B_CoreReviewArea;
    [SerializeField] private BossDropEnergy B_DropEnergy;
    [SerializeField] private BossFireBall B_ShootFireball;
    [SerializeField] private BossSpawnEnemy B_SpawnEnemy;
    [SerializeField] private BossWarpPlayer B_WarpPlayer;
    [SerializeField] private BossControlP1 B_Control1;
    [SerializeField] private HitPoints HP;
    [SerializeField] private Animator _anim;

    [SerializeField] private GameObject prop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //WalkAnimation();

        //AttackAnimation();

        //HurtAnimation();

        //DeadAnimation();

        FallAnimation();

        CallAnimation();

    }

    private void FallAnimation()
    {
        if(HP._CurrentHitPoints <= 0 || B_Control1.IsStun)
        {
            _anim.SetBool("IsFall", true);
            prop.SetActive(false);
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
        if(B_ShootFireball.enabled)
        {
            _anim.SetBool("IsCall", true);
            //prop.SetActive(false);
        }
        else
        {
            _anim.SetBool("IsCall", false);
            //prop.SetActive(true);
        }
    }

    private void WalkAnimation()
    {
        //if (E_P.enabled || E_MT.enabled && E_MT._Distance != 0)
        //{
        //    _anim.SetBool("IsWalking", true);
        //}
        //else
        //{
        //    _anim.SetBool("IsWalking", false);
        //}
    }

    private void AttackAnimation()
    {
        //if (E_MA.enabled)
        //{
        //    if (E_MA.IsCharging)
        //    {
        //        _anim.SetBool("IsCharge", true);
        //    }
        //    if (E_MA.IsAttacking)
        //    {
        //        _anim.SetBool("IsCharge", false);
        //        _anim.SetBool("IsAttack", true);
        //    }
        //    if (!E_MA.IsAttacking)
        //    {
        //        _anim.SetBool("IsAttack", false);
        //    }
        //}

    }
    private void HurtAnimation()
    {
        //if (HP._IsTakingDamage && !E_WC._IsDead)
        //{
            StartCoroutine(Hurt());
        //}
    }

    private IEnumerator Hurt()
    {
        //_anim.SetBool("IsTakingDamage", true);
        yield return new WaitForSeconds(0.25f);
        //_anim.SetBool("IsTakingDamage", false);
        //HP._IsTakingDamage = false;
    }

    private void DeadAnimation()
    {
        //if (E_WC._IsDead)
        //{
        //    _anim.SetTrigger("Die");
        //}
    }

    private void Die()  //Use in animation
    {
        Destroy(gameObject);
    }

    public void ActiveProp()
    {
        prop.SetActive(true);
    }
}
