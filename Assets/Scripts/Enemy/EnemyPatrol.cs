using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [HideInInspector]
    public float _WalkSpeed = 0;    //Use in Enemy control

    [Header("Set up")]
    [SerializeField] private Rigidbody2D _enemyRigidBody;
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private Transform _wallCheckPos;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private bool _onGround;
    [SerializeField] private bool _isHitWall;
    [SerializeField] private bool _isHitEnemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        //Debug.Log(_WalkSpeed);
    }

    private void FixedUpdate()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheckPos.position, 0.1f, _groundLayer); //Check is a ground circle is overlap with ground
        _isHitWall = Physics2D.OverlapCircle(_wallCheckPos.position, 0.1f, _wallLayer);   //Check is a wall circle hit ground layer
        _isHitEnemy = Physics2D.OverlapCircle(_wallCheckPos.position, 0.1f, _enemyLayer);   //Check is a wall circle hit ground layer
    }

    private void Patrol()
    {
        if (!_onGround || _isHitWall || _isHitEnemy)
        {
            Flip();
        }

        _enemyRigidBody.velocity = new Vector2(_WalkSpeed, _enemyRigidBody.velocity.y); //Walk
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        _WalkSpeed *= -1;
    }
}
