using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;

    private bool _onGround;
    private bool _isHitWall;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private Transform _wallCheckPos;
    [SerializeField] private LayerMask _groundLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void FixedUpdate()
    {
        _onGround = !Physics2D.OverlapCircle(_groundCheckPos.position, 0.1f, _groundLayer); //Check is a ground circle is not overlap with ground
        _isHitWall = Physics2D.OverlapCircle(_wallCheckPos.position, 0.1f, _groundLayer);   //Check is a wall circle hit ground layer
    }

    private void Patrol()
    {
        if (_onGround || _isHitWall == true)
        {
            Flip();
        }

        _rb.velocity = new Vector2(_walkSpeed, _rb.velocity.y); //Walk
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        _walkSpeed *= -1;
    }
}
