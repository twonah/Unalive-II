using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _durationBeforeAttack;
    [SerializeField] private float _durationAfterAttack;
   
    private float _beforeAttackTime;
    private float _afterAttackTime;

    private float _currentTime;

    [SerializeField] private Animator _anim;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _targetLayer;

    public bool _PlayerEnterAttackRange;
    public bool _ChargeOn;

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
            if (_currentTime >= _afterAttackTime + _durationAfterAttack && !_ChargeOn)
            {
                _anim.SetTrigger("ChargeAttack");
                _ChargeOn = true;
                _beforeAttackTime = _durationBeforeAttack;

                _anim.SetBool("AttackCooldown", true);
            }
        }

        ChargeTimer();

    }

    private void ChargeTimer()
    {
        if(!_ChargeOn)
        {
            return;
        }
        
        //_chargeOn == true
        if(_beforeAttackTime > 0)
        {
            _beforeAttackTime -= Time.deltaTime;
        }
        else
        {
            MeleeAttack();

            _afterAttackTime = _currentTime;
            _ChargeOn = false;

            _anim.SetTrigger("Attack");
        }
    }

    private void PlayerEnterAttackRange()
    {
        _PlayerEnterAttackRange = Physics2D.OverlapCircle(_attackPoint.position, _attackRange, _targetLayer);
    }

    private void MeleeAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _targetLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<HitPoints>().TakeDamage(_attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
