using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAnimation : MonoBehaviour
{
    //[SerializeField] private EnemyRunAway E_R;
    [SerializeField] private EnemySummon E_S;
    [SerializeField] private MageControl E_MC;
    [SerializeField] private HitPoints HP;
    [SerializeField] private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //WalkAnimation();

        SummonAnimation();

        HurtAnimation();

        DeadAnimation();
    }

    private void WalkAnimation()
    {
        //if (E_P.enabled || E_MT.enabled && E_MT._MoveToSpeed != 0)
        //{
        //    _anim.SetBool("IsWalking", true);
        //}
        //else
        //{
        //    _anim.SetBool("IsWalking", false);
        //}
    }

    private void SummonAnimation()
    {
        if(E_S.RandomSummon)
        {   
            if (E_S.index == 0)
            {
                _anim.SetBool("IsSummonW", true);
            }
            else
            {
                _anim.SetBool("IsSummonE", true);
            }
        }

        if(E_S.RandomSummon == false)
        {
            _anim.SetBool("IsSummonE", false);
            _anim.SetBool("IsSummonW", false);
        }

    }
    private void HurtAnimation()
    {
        if (HP._IsTakingDamage && !E_MC._IsDead)
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
        if (E_MC._IsDead)
        {
            _anim.SetTrigger("Die");
        }
    }

    private void Die()  //Use in animation
    {
        Destroy(gameObject);
    }
}
