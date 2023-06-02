using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControlP1 : MonoBehaviour
{

    [SerializeField] private float _eyeRange;
    [SerializeField] private float _stunTime;
    //[SerializeField] private float maxHP_Phase1;
    //[SerializeField] private float currentHP_Phase1;

    [SerializeField] private BossCoreReview B_CoreReview;
    [SerializeField] private BossCoreReviewArea B_CoreReviewArea;
    [SerializeField] private BossDropEnergy B_DropEnergy;
    [SerializeField] private BossFireBall B_ShootFireball;
    [SerializeField] private BossSpawnEnemy B_SpawnEnemy;
    [SerializeField] private BossWarpPlayer B_WarpPlayer;
    [SerializeField] private FaceDirectionCheck Self_FC;
    [SerializeField] private HitPoints HP_Boss;
    [SerializeField] private HitPoints HP_Core;

    [SerializeField] private Transform _eyePoint;

    [SerializeField] private LayerMask _playerAndDreamformLayer;    //Player and Dreamform layer
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _dreamformLayer;

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
    public bool EnterP2 = false;
    public bool CountdownStart;

    public bool _IsDead;

    private float countdownTimer;

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
    }

    // Update is called once per frame
    void Update()
    {
        _isDreamform = _SwitchControl.GetComponent<Controll_Script>().isDreamform;
        _playerHealth = _player.GetComponent<HitPoints>()._CurrentHitPoints;
        _dreamformHealth = _dreamform.GetComponent<HitPoints>()._CurrentHitPoints;

        BossHPUpdate();

        BossDetection();

        TargetSelect();

        BossPhase1();

    }

    private void BossHPUpdate()
    {
        if (B_CoreReview.IsReviewCore)
        {
            HP_Boss._maxHitPoints = HP_Core._maxHitPoints;
            HP_Boss._CurrentHitPoints = HP_Core._CurrentHitPoints;
            HP_Boss.enabled = false;
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
            //_followTarget = _playerPosition.transform;
        }
        else
        {
            _targetHealth = _dreamformHealth;
            //_followTarget = _dreamformPosition.transform;
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

    private void BossPhase1()
    {
        if(!B_CoreReviewArea.IsEnterArea)
        {
            B_CoreReview.IsReviewCore = false;
            B_DropEnergy.enabled = false;
            B_SpawnEnemy.enabled = false;
            CountdownStart = false;
            IsStun = false;

            if (!_see || EnterP2)
            {
                B_ShootFireball.enabled = false;
            }
            else if (_targetHealth > 0)  //see
            {
                B_ShootFireball.enabled = true;
            }
        }

        if(B_CoreReviewArea.IsEnterArea && HP_Boss._CurrentHitPoints > 0)
        {
            B_ShootFireball.enabled = false;
            B_CoreReview.enabled = true;
            B_CoreReview.IsReviewCore = true;
            B_DropEnergy.enabled = true;
            IsStun = true;
        }

        if(IsStun && HP_Boss._CurrentHitPoints > 0 && !B_WarpPlayer.IsWarp)
        {
            if(!CountdownStart)
            {   //Start countdown
                B_SpawnEnemy.enabled = true;
                countdownTimer = Time.time + _stunTime;
                CountdownStart = true;

                //Debug.Log("Stun : Countdown start");
            }
            else
            {
                if (Time.time >= countdownTimer && HP_Boss._CurrentHitPoints > 0)   //Cant finish in time
                {
                    IsStun = false;
                    B_SpawnEnemy.enabled = false;
                    CountdownStart = false;
                    B_WarpPlayer.enabled = true;
                    //Debug.Log("Warp player back");
                }

            }
        }
        else
        {
            B_WarpPlayer.enabled = false;
            B_WarpPlayer.IsWarp = false;
        }

        if (HP_Boss._CurrentHitPoints <= 0 || HP_Core._CurrentHitPoints <= 0)  //Enter Phase 2
        {
            IsStun = false;
            B_SpawnEnemy.enabled = false;
            CountdownStart = false;
            EnterP2 = true;
            B_CoreReview.IsReviewCore = false;
            //Debug.Log("Enter P2");
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (_eyePoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_eyePoint.position, _eyeRange);
    }
}
