using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageControl : MonoBehaviour
{
    [SerializeField] private float _checkTime;
    [SerializeField] private float _summonDelay;

    [SerializeField] private float _eyeRange;

    [SerializeField] private EnemySummon E_Summon;
    [SerializeField] private EnemyAreaCheck E_AreaCheck;
    //[SerializeField] private EnemyMoveToPlayer E_RunAway;
    [SerializeField] private FaceDirectionCheck Self_FC;
    [SerializeField] private HitPoints HP;

    [SerializeField] private GameObject summonObject;
    [SerializeField] private GameObject[] summonList;

    [SerializeField] private Transform summonSpawnpoint;
    [SerializeField] private Transform mageTransform;

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

    public bool _IsDead;


    // Start is called before the first frame update
    void Start()
    {
        E_AreaCheck.CheckTime = _checkTime;
        E_Summon.SummonSpawnpoint = summonSpawnpoint;
        E_Summon.SummonList = summonList;
        E_Summon.SummonObject = summonObject;
        E_Summon.SummonDelay = _summonDelay;
        E_Summon.MageTransform = mageTransform;

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

        MageDetection();

        if (_enemyHP <= 0)   //Dead
        {
            _IsDead = true;
        }

        MageControls();

        TargetSelect();

        //Debug.Log("Distance x" + E_MoveTo._Distance + "Distance y "+ E_MoveTo._DistanceY);

    }

    private void TargetSelect()
    {
        if (!_isDreamform)
        {
            _targetHealth = _playerHealth;
            _followTarget = _playerPosition.transform;
            //E_Attack._TargetLayer = _playerLayer;
        }
        else
        {
            _targetHealth = _dreamformHealth;
            _followTarget = _dreamformPosition.transform;
            //E_Attack._TargetLayer = _dreamformLayer;
        }
    }

    private void MageDetection()
    {
        _see = Physics2D.OverlapCircle(_eyePoint.position, _eyeRange, _playerAndDreamformLayer);    //Is there any collider with Player layer

        if (_targetHealth <= 0)
        {
            _see = false;
        }
    }

    private void MageControls()
    {
        if (!_see)  //Player not enter eye range
        {
            E_AreaCheck.enabled = true;
            //E_Summon.enabled = false;
        }
        else if (_see && _targetHealth > 0)  //Player enter eye range 
        {
            //E_Summon.enabled = true;
            E_AreaCheck.enabled = false;
            if(Time.time > E_Summon.nextSummon)
            {
                E_Summon.isSummon = true;
            }
        }

        if (_see && _targetHealth <= 0)
        {
            E_AreaCheck.enabled = true;
            E_Summon.isSummon = false;
        }

        if (_IsDead)
        {
            E_Summon.enabled = false;
            E_AreaCheck.enabled = false;
        }

        //if(!_see)
        //{
        //    if(E_Summon.RandomSummon)
        //    {
        //        E_Summon.enabled = true;
        //    }
        //}

        //StartCoroutine(TargetHealthCheck());
    }

    private IEnumerator SummonControl()
    {
        E_Summon.isSummon = true;
        yield return new WaitForSeconds(0.2f);
        E_Summon.isSummon = false;
    }

    private IEnumerator TargetHealthCheck()
    {
        yield return new WaitForSeconds(0.3f);

        if (_targetHealth <= 0)
        {
            //E_Shoot.enabled = false;
        }
        else if (_see && _targetHealth > 0)
        {
            //E_Shoot.enabled = true;
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
