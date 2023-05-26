using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelderControl : MonoBehaviour
{
    [SerializeField] private float _patrolWalkSpeed;
    [SerializeField] private float _moveToTargetWalkSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _durationBeforeAttack;
    [SerializeField] private float _durationAfterAttack;

    [SerializeField] private float _eyeRange;

    [SerializeField] private EnemyPatrol E_Patrol;
    [SerializeField] private EnemyMeleeAttack E_Attack;
    [SerializeField] private EnemyMoveToPlayer E_MoveTo;
    [SerializeField] private FaceDirectionCheck Self_FC;
    [SerializeField] private HitPoints HP;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _eyePoint;
    [SerializeField] private LayerMask _playerAndDreamformLayer;    //Player and Dreamform layer
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _dreamformLayer;

    private GameObject _SwitchControl;
    private GameObject _player;
    private GameObject _dreamform;

    private bool _isDreamform;
    public float _targetHealth;
    private float _playerHealth;
    private float _dreamformHealth;
    private float _enemyHP;

    private Transform _followTarget;
    private Transform _playerPosition;
    private Transform _dreamformPosition;

    private bool _see = false;

    public bool _IsDead;


    // Start is called before the first frame update
    void Start()
    {
        E_Patrol._WalkSpeed = _patrolWalkSpeed;
        E_MoveTo._MoveToSpeed = _moveToTargetWalkSpeed;
        E_Attack._AttackRange = _attackRange;
        E_Attack._AttackDamage = _attackDamage;
        E_Attack._DurationAfterAttack = _durationAfterAttack;
        E_Attack._DurationBeforeAttack = _durationBeforeAttack;

        _SwitchControl = GameObject.FindWithTag("SwitchControl");
        _player = GameObject.FindWithTag("Player");
        _dreamform = GameObject.FindWithTag("DreamForm");
    }

    // Update is called once per frame
    void Update()
    {
        _enemyHP = HP._CurrentHitPoints;

        _isDreamform = _SwitchControl.GetComponent<Controll_Script>().isDreamform;
        _playerHealth = _player.GetComponent<HitPoints>()._CurrentHitPoints;
        _dreamformHealth = _dreamform.GetComponent<HitPoints>()._CurrentHitPoints;

        _playerPosition = _player.GetComponent<Transform>();
        _dreamformPosition = _dreamform.GetComponent<Transform>();

        E_MoveTo._Target = _followTarget;

        WelderDetection();

        if(_enemyHP <= 0)   //Dead
        {
            _IsDead = true;
        }

        if(_targetHealth > 0)    //
        {
            WelderControls();
        }

        

        TargetSelect();

    }

    private void TargetSelect()
    {
        if(!_isDreamform)
        {
            _targetHealth = _playerHealth;
            _followTarget = _playerPosition.transform;
            E_Attack._TargetLayer = _playerLayer;
        }
        else
        {
            _targetHealth = _dreamformHealth;
            _followTarget = _dreamformPosition.transform;
            E_Attack._TargetLayer = _dreamformLayer;
        }
    }

    private void WelderDetection()
    {
        _see = Physics2D.OverlapCircle(_eyePoint.position, _eyeRange, _playerAndDreamformLayer);    //Is there any collider with Player layer
    }

    private void WelderControls()
    {
        if (!_see && E_Attack._ChargeOn == false && !E_Attack.IsCharging && !E_Attack.IsAttacking)  //Player not enter eye range
        {
            E_Patrol.enabled = true;
            E_MoveTo.enabled = false;
        }

        if (_see)    //Player enter eye range 
        {
            E_MoveTo.enabled = true;
            E_Patrol.enabled = false;
            if(E_Attack.IsCharging || E_Attack.IsAttacking)
            {
                E_MoveTo.enabled = false;
                E_Patrol.enabled = false;
            }
        }

        if (E_Attack._PlayerEnterAttackRange == true || E_Attack._ChargeOn == true)    //Player enter attack range
        {
            E_MoveTo.enabled = false;
            E_Patrol.enabled = false;
        }

        if(_IsDead)
        {
            E_Attack.enabled = false;
            E_Patrol.enabled = false;
            E_MoveTo.enabled = false;
        }

        StartCoroutine(TargetHealthCheck());

        //Additional check
        if (E_Patrol._WalkSpeed <= -0.1 && Self_FC._FacingRight && !E_Patrol.enabled)
        {
            E_Patrol._WalkSpeed *= -1;
        }

        if (E_Patrol._WalkSpeed >= 0.1 && Self_FC._FacingLeft && !E_Patrol.enabled)
        {
            E_Patrol._WalkSpeed *= -1;
        }
    }

    private IEnumerator TargetHealthCheck()
    {
        yield return new WaitForSeconds(0.3f);

        if (_targetHealth <= 0)
        {
            E_Attack.enabled = false;
        }
        else
        {
            E_Attack.enabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(_eyePoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_eyePoint.position, _eyeRange);
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
