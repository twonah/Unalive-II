using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamFormAnimator : MonoBehaviour
{
    [SerializeField] private Controll_Script SwitchControls;
    [SerializeField] private DreamForm_Movement DM;
    [SerializeField] private DreamForm_Punch DP;
    [SerializeField] private HitPoints HP;
    [SerializeField] private Animator _anim;

    private float horizontal;
    private bool _isDead;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WalkAnimation();

        HurtAnimation();

        DeadAnimation();

        TransformAnimation();

        AttackAnimation();

        PhaseDashAnimation();

        Die();
    }
    private void AttackAnimation()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _anim.SetTrigger("Attack");
        }
    }
    private void WalkAnimation()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        _anim.SetFloat("Speed", Mathf.Abs(horizontal));
    }
    private void HurtAnimation()
    {
        if (HP._IsTakingDamage)
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
    private void DeadAnimation()    //Done?
    {
        if (HP._CurrentHitPoints <= 0)
        {
            _anim.SetTrigger("Die");
        }
    }

    private void PhaseDashAnimation()
    {
        if (DM.isDashingg)
        {
            _anim.SetBool("IsDash", true);
        }

        if (!DM.isDashingg)
        {
            _anim.SetBool("IsDash", false);
        }
    }
    private void TransformAnimation()
    {
        StartCoroutine(TransformDelay());
    }

    private IEnumerator TransformDelay()
    {
        if (SwitchControls.isDreamWalkerToDreamform )    //Player to Dream
        {
            _anim.SetBool("IsTransform", true);
            _anim.SetBool("IsDreamform", true);
            yield return new WaitForSeconds(0.5f);
            _anim.SetBool("IsTransform", false);
        }

        if (SwitchControls.isDreamWalkerToPlayer && !SwitchControls._IsDreamformDead)   //Dream to player
        {
            _anim.SetBool("IsTransform", true);
            _anim.SetBool("IsDreamform", false);
            yield return new WaitForSeconds(0.5f);
            _anim.SetBool("IsTransform", false);
        }
    }

    private void Die()
    {
        if(HP._CurrentHitPoints <= 0)
        {
            _isDead = true;
        }

        if(_isDead)
        {
            _anim.SetTrigger("Die");
        }
    }

}
