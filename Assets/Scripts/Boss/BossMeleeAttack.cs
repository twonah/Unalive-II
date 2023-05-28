using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    [Header("Set up")]
    [HideInInspector] public float _AttackRange;
    [SerializeField] public float _AttackDamage;
    [SerializeField] public float _DurationBeforeAttack;
    [SerializeField] public float _DurationAfterAttack;
    [SerializeField] private Transform _attackPoint;

    [HideInInspector] public LayerMask _TargetLayer;

    [HideInInspector] public bool _PlayerEnterAttackRange;
    [HideInInspector] public bool _ChargeOn;

    private float _beforeAttackTime;
    private float _afterAttackTime;
    private float _currentTime;

    public bool IsCharging;
    public bool IsAttacking;
    public bool IsCooldown;
    public bool AttackDone;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _currentTime = Time.time;

        PlayerEnterAttackRange();

        if (_PlayerEnterAttackRange == true)
        {
            if (_currentTime >= _afterAttackTime + _DurationAfterAttack && !_ChargeOn)
            {
                //_anim.SetTrigger("ChargeAttack");
                _ChargeOn = true;
                _beforeAttackTime = _DurationBeforeAttack;

                //_anim.SetBool("AttackCooldown", true);
            }
        }

        ChargeTimer();

    }

    private void ChargeTimer()
    {
        if (!_ChargeOn)
        {
            return;
        }

        //_chargeOn == true
        if (_beforeAttackTime > 0)
        {
            _beforeAttackTime -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(NewMeleeAttack());
            //MeleeAttack();

            _afterAttackTime = _currentTime;
            _ChargeOn = false;

            //_anim.SetTrigger("Attack");
        }
    }

    private void PlayerEnterAttackRange()
    {
        _PlayerEnterAttackRange = Physics2D.OverlapCircle(_attackPoint.position, _AttackRange, _TargetLayer);
    }

    private void OnDisable()
    {
        AttackDone = false;
    }

    private IEnumerator NewMeleeAttack()
    {
        IsCharging = true;
        yield return new WaitForSeconds(_DurationBeforeAttack);

        IsCharging = false;
        IsAttacking = true;
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _AttackRange, _TargetLayer);
        foreach (Collider2D player in hitPlayer)
        {
            //Debug.Log(player.gameObject.name);
            player.GetComponent<HitPoints>().TakeDamage(_AttackDamage);
        }
        IsCooldown = true;
        yield return new WaitForSeconds(_DurationAfterAttack);
        IsAttacking = false;
        IsCooldown = false;
        AttackDone = true;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (_attackPoint == null)
    //    {
    //        return;
    //    }

    //    Gizmos.DrawWireSphere(_attackPoint.position, _AttackRange);
    //}
}
