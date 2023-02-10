using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Saw type")]
    [SerializeField] private bool MoveableSaw;
    [SerializeField] private bool StaySaw;

    [Header("General")]
    [SerializeField] private Rigidbody2D _sawRB;
    [SerializeField] private float _damage;
    private GameObject _player;

    [Header("Moveable Saw")]
    [SerializeField] private Transform _fisrtPos;
    [SerializeField] private Transform _secondPos;
    [SerializeField] private Transform _nextPos;

    [SerializeField] private float _moveSpeed = 0f;
    [SerializeField] private float stayDuration;

    private bool _timeOn;
    private float _nextMoveTime;
    private bool _isMove;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _isMove = true;

        _nextPos.transform.position = _fisrtPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveableSaw)
        {
            Moveable_Saw();
        }

        if(StaySaw)
        {
            Stay_Saw();
        }
       
        Timer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))      //Deal damage to the player
        {
            _player.GetComponent<HitPoints>().TakeDamage(_damage);
            Debug.Log("Hurt");
        }
    }

    private void Timer()
    {
        if(_timeOn)
        {
            if(Time.time > _nextMoveTime)
            {
                _timeOn = false;
                _isMove = true;
            }
        }
    }
    private void Moveable_Saw()
    {
        if (!_timeOn)
        {
            _sawRB.transform.position = Vector2.MoveTowards(_sawRB.transform.position, new Vector2(_nextPos.position.x, _nextPos.position.y), _moveSpeed * Time.deltaTime);
        }

        if (_isMove)
        {
            if(_sawRB.transform.position == _fisrtPos.transform.position)
            {
                _isMove = false;
                _timeOn = true;
                _nextPos.transform.position = _secondPos.transform.position;
            }

            if (_sawRB.transform.position == _secondPos.transform.position)
            {
                _isMove = false;
                _timeOn = true;
                _nextPos.transform.position = _fisrtPos.transform.position;
            }

            _nextMoveTime = stayDuration + Time.time;
        }
    }
    private void Stay_Saw()
    {
        //Do nothing
    }
}
