using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToPlayer : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _distance;

    private bool _onGround;
    private bool _isHitWall;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private Transform _wallCheckPos;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        //Flip();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();

        _distance = _target.transform.position.x - _rb.transform.position.x;

        if (_distance == 0)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        _onGround = !Physics2D.OverlapCircle(_groundCheckPos.position, 0.1f, _groundLayer); //Check is a ground circle is not overlap with ground
        _isHitWall = Physics2D.OverlapCircle(_wallCheckPos.position, 0.1f, _groundLayer);   //Check is a wall circle hit ground layer

    }

    private void MoveToPlayer()
    {
        

        if (_onGround == false)
        {
            //_rb.transform.position = new Vector2(_rb.velocity.x, _rb.velocity.y);
            //_rb.transform.position = Vector2.MoveTowards(_rb.transform.position, new Vector2(_target.position.x, transform.position.y), _walkSpeed * Time.deltaTime);
        }

        
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        //_walkSpeed *= -1;
    }

}
