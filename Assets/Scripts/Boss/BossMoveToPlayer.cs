using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveToPlayer : MonoBehaviour
{
    [SerializeField] private float _moveToSpeed;
    [HideInInspector] public float _Distance;
    [HideInInspector] public float _DistanceY;

    [SerializeField] private Rigidbody2D _enemyRigidBody;
    [SerializeField] public Transform _Target;

    [SerializeField] private FaceDirectionCheck Self_FC;

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

        if (_Distance == 0)
        {
            return;
        }

        if (_Distance >= 0 && Self_FC._FacingLeft)
        {
            Flip();
        }
        if (_Distance <= 0 && Self_FC._FacingRight)
        {
            Flip();
        }
    }

    private void MoveToPlayer()
    {
        _enemyRigidBody.transform.position = Vector2.MoveTowards(_enemyRigidBody.transform.position, new Vector2(_Target.position.x, _Target.position.y), _moveToSpeed * Time.deltaTime);
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
