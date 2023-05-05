using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToPlayer : MonoBehaviour
{
    [HideInInspector] public float _MoveToSpeed;
    [HideInInspector] public float _Distance;
    [HideInInspector] public float _DistanceY;

    [Header("Set up")]
    [SerializeField] private FaceDirectionCheck Self_FC;
    [SerializeField] private Rigidbody2D _enemyRigidBody;
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private Transform _wallCheckPos;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;

    [HideInInspector] public Transform _Target;

    private bool _onGround;
    private bool _isHitWall;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        MoveToPlayer();

        _Distance = _Target.transform.position.x - _enemyRigidBody.transform.position.x;
        _DistanceY = _Target.transform.position.y - _enemyRigidBody.transform.position.y;

        //Not Done yet??
        if (_Distance == 0)
        {
            return;
        }

        if (_Distance >= 0 && Self_FC._FacingLeft)
        {
            Flip();
        }
        if(_Distance <= 0 && Self_FC._FacingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheckPos.position, 0.1f, _groundLayer); //Check is a ground circle is overlap with ground
        _isHitWall = Physics2D.OverlapCircle(_wallCheckPos.position, 0.1f, _wallLayer);   //Check is a wall circle hit ground layer
    }

    private void MoveToPlayer()
    {
        if (_onGround == true)
        {
            _enemyRigidBody.transform.position = Vector2.MoveTowards(_enemyRigidBody.transform.position, new Vector2(_Target.position.x, transform.position.y), _MoveToSpeed * Time.deltaTime);
        }
        else if(_isHitWall)
        {
            //Done, just not move if hit wall even chase a target
        }
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

}
