using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;

    private GameObject _SwitchControl;
    private GameObject _player;
    private GameObject _dreamform;
    private GameObject _targetOb;

    private Transform _playerPosition;
    private Transform _dreamformPosition;
    private Transform _targetPos;

    private string playerTag = "Player";
    private string dreamformTag = "DreamForm";

    [SerializeField] private float rotateSpeed;

    [SerializeField] private float rotationModifier;

    private bool _isDreamform;

    void Start()
    {
        _SwitchControl = GameObject.FindWithTag("SwitchControl");
        _player = GameObject.FindWithTag("Player");
        _dreamform = GameObject.FindWithTag("DreamForm");

        _playerPosition = _player.GetComponent<Transform>();
        _dreamformPosition = _dreamform.GetComponent<Transform>();

        _isDreamform = _SwitchControl.GetComponent<Controll_Script>().isDreamform;

        TargetSelect();

        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
    }
    private void FixedUpdate()
    {
        if (_targetOb != null)
        {
            Vector3 vectorToTarget = _targetOb.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
        }

    }

    private void TargetSelect()
    {
        if (!_isDreamform)
        {
            _targetPos = _playerPosition.transform;
            _targetOb = _player;
        }
        else
        {
            _targetPos = _dreamformPosition.transform;
            _targetOb = _dreamform;
        }
    }

    private void MoveTo()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(_targetPos.position.x, _targetPos.position.y), _moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            HitPoints playerHP = collision.gameObject.GetComponent<HitPoints>();

            if (playerHP != null)
            {
                playerHP.TakeDamage(_damage);
            }
        }

        if (collision.gameObject.CompareTag(dreamformTag))
        {
            HitPoints dreamFormHP = collision.gameObject.GetComponent<HitPoints>();

            if (dreamFormHP != null)
            {
                dreamFormHP.TakeDamage(_damage);
            }
        }

        Destroy(gameObject);
    }


}
