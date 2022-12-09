using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToPlayer : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _distance;

    [SerializeField] private FaceDirectionCheck Self_FC;

    [SerializeField] private Controll_Script CS;

    private bool _onGround;
    private bool _isHitWall;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private Transform _wallCheckPos;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private Transform _target;

    [SerializeField] private Transform _physicalForm;
    [SerializeField] private Transform _dreamForm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TargetSelect();

        MoveToPlayer();

        _distance = _target.transform.position.x - _rb.transform.position.x;
        //Not Done yet
        if(_distance == 0)
        {
            return;
        }

        if (_distance >= 0 && Self_FC._FacingLeft)
        {
            Flip();
        }
        if(_distance <= 0 && Self_FC._FacingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        _onGround = Physics2D.OverlapCircle(_groundCheckPos.position, 0.1f, _groundLayer); //Check is a ground circle is overlap with ground
        _isHitWall = Physics2D.OverlapCircle(_wallCheckPos.position, 0.1f, _groundLayer);   //Check is a wall circle hit ground layer

    }

    private void MoveToPlayer()
    {
        if (_onGround == true)
        {
            _rb.transform.position = Vector2.MoveTowards(_rb.transform.position, new Vector2(_target.position.x, transform.position.y), _walkSpeed * Time.deltaTime);
        }
    }

    private void TargetSelect()
    {
        if(CS.isDreamWalker)
        {
            _target = _dreamForm.transform;
        }
        if(!CS.isDreamWalker)
        {
            _target = _physicalForm.transform;
        }
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

}
