using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControlP2 : MonoBehaviour
{
    [SerializeField] private float _eyeRange;
    [SerializeField] private float _stunTime;

    [SerializeField] private BossCoreReview B_CoreReview;
    [SerializeField] private BossFlyAround B_Fly;
    [SerializeField] private BossMoveToPlayer B_MoveTo;
    [SerializeField] private BossMeleeAttack B_MeleeAttack;
    [SerializeField] private BossSummonLaser B_LaserSummon;
    [SerializeField] private FaceDirectionCheck Self_FC;
    [SerializeField] private HitPoints HP_Boss;
    [SerializeField] private HitPoints HP_Core;

    [SerializeField] private Transform _eyePoint;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] public float _attackRange;

    [SerializeField] private LayerMask _playerAndDreamformLayer;    //Player and Dreamform layer
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _dreamformLayer;

    [HideInInspector] public LayerMask _targetLayer;

    [SerializeField] public bool _PlayerEnterAttackRange;

    private GameObject _SwitchControl;
    private GameObject _player;
    private GameObject _dreamform;

    private bool _isDreamform;
    private float _targetHealth;
    private float _playerHealth;
    private float _dreamformHealth;
    private float _enemyHP;

    private Transform _followTarget;
    private Transform _playerPosition;
    private Transform _dreamformPosition;

    public bool _see = false;

    public bool IsStun = false;
    public bool EnterP3 = false;
    public bool CountdownStart;

    private float countdownTimer;

    public int randomAttack;
    public bool isRandomAttack;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _SwitchControl = GameObject.FindWithTag("SwitchControl");
        _player = GameObject.FindWithTag("Player");
        _dreamform = GameObject.FindWithTag("DreamForm");

        B_MeleeAttack._AttackRange = _attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        _isDreamform = _SwitchControl.GetComponent<Controll_Script>().isDreamform;
        _playerHealth = _player.GetComponent<HitPoints>()._CurrentHitPoints;
        _dreamformHealth = _dreamform.GetComponent<HitPoints>()._CurrentHitPoints;

        _playerPosition = _player.GetComponent<Transform>();
        _dreamformPosition = _dreamform.GetComponent<Transform>();

        BossHPUpdate();

        BossDetection();

        TargetSelect();

        BossPhase2();

        PlayerEnterAttackRange();

        B_MoveTo._Target = _followTarget;
    }

    private void BossHPUpdate()
    {
        if (B_CoreReview.IsReviewCore)  //Show core
        {
            //HP_Boss._maxHitPoints = HP_Core._maxHitPoints;
            //HP_Core._CurrentHitPoints = HP_Boss._CurrentHitPoints;
            //HP_Boss.enabled = false;
        }
        else  //Not show core
        {
            HP_Core._maxHitPoints = HP_Boss._maxHitPoints;
            HP_Core._CurrentHitPoints = HP_Boss._CurrentHitPoints;
            HP_Boss.enabled = true;
        }

        if (HP_Boss._IsTakingDamage || HP_Core._IsTakingDamage)
        {
            audioManager.PlaySFX(audioManager.bosshurt);
        }
    }

    private void TargetSelect()
    {
        if (!_isDreamform)
        {
            _targetHealth = _playerHealth;
            _followTarget = _playerPosition.transform;
            B_MeleeAttack._TargetLayer = _playerLayer;
            _targetLayer = _playerLayer;
        }
        else
        {
            _targetHealth = _dreamformHealth;
            _followTarget = _dreamformPosition.transform;
            B_MeleeAttack._TargetLayer = _dreamformLayer;
            _targetLayer = _dreamformLayer;
        }
    }

    private void BossDetection()
    {
        _see = Physics2D.OverlapCircle(_eyePoint.position, _eyeRange, _playerAndDreamformLayer);    //Is there any collider with Player layer

        if (_targetHealth <= 0)
        {
            _see = false;
        }
    }

    private void PlayerEnterAttackRange()
    {
        _PlayerEnterAttackRange = Physics2D.OverlapCircle(_attackPoint.position, _attackRange, _targetLayer);
    }
    private void BossPhase2()
    {
        if (!EnterP3 || !IsStun)
        {
            if (!_see && !B_MeleeAttack.IsCharging && !B_MeleeAttack.IsAttacking && !B_LaserSummon._IsSummonLaser && !IsStun && !B_CoreReview.IsCoreOut)   //Player not in eye range
            {
                B_Fly.enabled = true;
                B_MoveTo.enabled = false;
                //Debug.Log("Dont see");
            }
            else if(_see && B_Fly._IsHitWall)
            {
                _see = false;
                //Debug.Log("see but wall");
            }

            if(_see && !B_Fly._IsHitWall)
            {
                //Debug.Log("see");
                if (!_PlayerEnterAttackRange && !B_MeleeAttack.IsCharging && !B_MeleeAttack.IsAttacking && !B_LaserSummon._IsSummonLaser && !IsStun && !B_CoreReview.IsCoreOut)
                {
                    B_MoveTo.enabled = true;
                    B_Fly.enabled = false;
                }
                
                if(_PlayerEnterAttackRange) //random attack
                {
                    B_MoveTo.enabled = false;
                    //Debug.Log("Attack");

                    if (!isRandomAttack)
                    {
                        randomAttack = Random.Range(0, 2);
                        isRandomAttack = true;
                    }

                    if (randomAttack == 0 && isRandomAttack)       // Melee Attack
                    {
                        B_MeleeAttack.enabled = true;
                        //Debug.Log("Melee Attack");
                        if (B_MeleeAttack.AttackDone)
                        {
                            B_MeleeAttack.enabled = false;
                            isRandomAttack = false;
                            //Debug.Log("Disable Melee Attack");
                        }
                    }
                    else if(randomAttack != 0 && isRandomAttack)        // Summon laser
                    {
                        B_LaserSummon.enabled = true;
                        //Debug.Log("Laser Attack");
                        if (B_LaserSummon.SummonDone)
                        {
                            B_LaserSummon.enabled = false;
                            isRandomAttack = false;
                            //Debug.Log("Disable Laser Attack");
                        }
                    }
                }
            }
        }
        if(EnterP3)        // After enter phase 3
        {
            B_Fly.enabled = false;
            B_LaserSummon.enabled = false;
            B_MeleeAttack.enabled = false;
            B_MoveTo.enabled = false;
            //Debug.Log("Boss Fall, enter 3");
        }

        if(!IsStun)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        if (HP_Boss._CurrentHitPoints <= 0 && !EnterP3)  //Boss fall
        {
            B_CoreReview.enabled = true;
            B_CoreReview.IsReviewCore = true;
            IsStun = true;

            if(IsStun)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

                if(!CountdownStart)     //Start Countdown
                {
                    countdownTimer = Time.time + _stunTime;
                    CountdownStart = true;
                    Debug.Log("Start Countdown");
                }

                if(CountdownStart)
                {
                    Debug.Log(countdownTimer + " | " + Time.time);

                    if (Time.time >= countdownTimer && HP_Boss._CurrentHitPoints >= HP_Core._CurrentHitPoints)    // Cant defeat in time
                    {
                        IsStun = false;
                        HP_Boss._CurrentHitPoints = HP_Boss._maxHitPoints;
                        CountdownStart = false;
                        B_CoreReview.IsReviewCore = false;
                        Debug.Log("Revive");
                    }

                    if(Time.time >= countdownTimer && HP_Core._CurrentHitPoints >= HP_Boss._CurrentHitPoints)       //Check in case Core > Boss
                    {
                        IsStun = false;
                        HP_Boss._CurrentHitPoints = HP_Boss._maxHitPoints;
                        CountdownStart = false;
                        B_CoreReview.IsReviewCore = false;
                        Debug.Log("Revive2");
                    }

                    if(Time.time <= countdownTimer &&  HP_Core._CurrentHitPoints < HP_Boss._CurrentHitPoints)  // Enter phase 3
                    {
                        EnterP3 = true;
                        B_CoreReview.IsReviewCore = false;
                        IsStun = false;
                        CountdownStart = false;
                        Debug.Log("Enter Phase 3");
                    }
                }
            }
        }

        //Additional Check ?????
        if (B_Fly._WalkSpeed <= -0.1 && Self_FC._FacingRight && !B_Fly.enabled)
        {
            B_Fly._WalkSpeed *= -1;
        }

        if (B_Fly._WalkSpeed >= 0.1 && Self_FC._FacingLeft && !B_Fly.enabled)
        {
            B_Fly._WalkSpeed *= -1;
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (_eyePoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_eyePoint.position, _eyeRange);

        if (_attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
