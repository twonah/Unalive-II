using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyAround : MonoBehaviour
{
    [SerializeField] public float _WalkSpeed = 0;

    [SerializeField] private Rigidbody2D _enemyRigidBody;
    [SerializeField] private Transform _wallCheckPos;
    [SerializeField] private LayerMask _wallLayer;

    [SerializeField] public bool _IsHitWall;

    private float walkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _WalkSpeed *= -1;

        walkSpeed = _WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Fly();

        walkSpeed = _WalkSpeed;

        //DirectionCheck();
    }

    private void FixedUpdate()
    {
        _IsHitWall = Physics2D.OverlapCircle(_wallCheckPos.position, 0.1f, _wallLayer);   //Check is a wall circle hit ground layer
    }

    private void OnEnable()
    {
        walkSpeed = _WalkSpeed;
        //Debug.Log("Enable : walkSpeed : " + walkSpeed);
    }
    private void OnDisable()
    {
        walkSpeed = 0;
        //Debug.Log("Disable : walkSpeed : " + walkSpeed);
        _enemyRigidBody.velocity = new Vector2(walkSpeed, _enemyRigidBody.velocity.y); //Walk
    }

    private void Fly()
    {
        if (_IsHitWall)
        {
            Flip();       
        }

        _enemyRigidBody.velocity = new Vector2(walkSpeed, _enemyRigidBody.velocity.y); //Walk
    }
    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        _WalkSpeed *= -1;
    }

    private void DirectionCheck()
    {
        if(transform.localScale.x < 0)
        {
            walkSpeed *= -1;
        }
        else
        {
            walkSpeed = Mathf.Abs(-10.5f);
        }
    }
}
