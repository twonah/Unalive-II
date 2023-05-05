using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerControl : MonoBehaviour
{
    [SerializeField] private float _patrolWalkSpeed;
    [SerializeField] private float _moveToTargetWalkSpeed;
    [SerializeField] private float _eyeRange;
    [SerializeField] private float _shootDistance;

    [SerializeField] private float bulletSpeed = 0f;
    [SerializeField] private float spawnDelay = 0f;

    [SerializeField] private EnemyPatrol E_Patrol;
    [SerializeField] private EnemyShoot E_Shoot;
    [SerializeField] private EnemyMoveToPlayer E_MoveTo;
    [SerializeField] private FaceDirectionCheck Self_FC;
    [SerializeField] private HitPoints HP;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] private Transform gunnerTransform;
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
        E_Patrol._WalkSpeed = _patrolWalkSpeed;
        E_MoveTo._MoveToSpeed = _moveToTargetWalkSpeed;
        E_Shoot.BulletSpeed = bulletSpeed;
        E_Shoot.SpawnDelay = spawnDelay;

        E_Shoot.BulletSpawnpoint = bulletSpawnpoint;
        E_Shoot.BulletPrefab = bulletPrefab;

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

        GunnerDetection();

        if (_enemyHP <= 0)   //Dead
        {
            _IsDead = true;
        }

        GunnerControls();

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

    private void GunnerDetection()
    {
        _see = Physics2D.OverlapCircle(_eyePoint.position, _eyeRange, _playerAndDreamformLayer);    //Is there any collider with Player layer

        if(_targetHealth <= 0)
        {
            _see = false;
        }
    }

    private void GunnerControls()
    {
        if (!_see)  //Player not enter eye range
        {
            E_Patrol.enabled = true;
            E_MoveTo.enabled = false;
            E_MoveTo._MoveToSpeed = _moveToTargetWalkSpeed;
        }
        else if(_see && _targetHealth > 0)  //Player enter eye range 
        {
            E_MoveTo.enabled = true;
            E_Patrol.enabled = false;

            if (E_MoveTo._DistanceY > 1 || E_MoveTo._DistanceY <= -2)
            {
                //Higher than enemy or below
                E_Shoot.enabled = false;
            }
            else if (E_MoveTo._Distance <= _shootDistance && E_MoveTo._DistanceY <= 1)
            {
                E_Shoot.enabled = true;
                E_MoveTo._MoveToSpeed = 0;
            }

        }

        if (_see && _targetHealth <= 0)
        {
            E_MoveTo.enabled = false;
            E_Patrol.enabled = true;
        }


        //if (_see)    //Player enter eye range 
        //{
        //    E_MoveTo.enabled = true;
        //    E_Patrol.enabled = false;
        //}
        //if(_see)
        //{
        //    if (E_MoveTo._Distance <= _shootDistance)
        //    {
        //        E_MoveTo._MoveToSpeed = 0;
        //        E_Shoot.enabled = true;
        //    }
        //}


        //if (E_Attack._PlayerEnterAttackRange == true || E_Attack._ChargeOn == true)    //Close player enough
        //{
        //    E_MoveTo.enabled = false;
        //}

        if (_IsDead)
        {
            E_Shoot.enabled = false;
            E_Patrol.enabled = false;
            E_MoveTo.enabled = false;
        }

        //StartCoroutine(TargetHealthCheck());

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
            E_Shoot.enabled = false;
        }
        else if(_see && _targetHealth > 0)
        {
            E_Shoot.enabled = true;
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
