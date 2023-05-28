using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private Animator _anim;

    [SerializeField] private float _chargeDuration;
    [SerializeField] private float _activeDuration;
    [SerializeField] private float _disableDuration;

    [SerializeField] private bool _isCharge;
    [SerializeField] private bool _isActive;

    private string playerTag = "Player";
    private string dreamformTag = "DreamForm";

    private float chargeTime;
    private float disableTime;
    private float closeTime;

    // Start is called before the first frame update
    void Start()
    {
        _isCharge = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        chargeTime = _chargeDuration + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        laserControl();
    }

    private void FixedUpdate()
    {
        //StartCoroutine(LaserControl());
    }

    private void OnEnable()
    {
        chargeTime = _chargeDuration + Time.time;
        disableTime = chargeTime + _activeDuration;
        closeTime = disableTime + _disableDuration;
    }

    private void laserControl()
    {
        if(Time.time >= chargeTime && Time.time < disableTime)
        {
            _anim.SetBool("Active", true);
            _isActive = true;
            _isCharge = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if(Time.time < chargeTime)
        {
            _anim.SetBool("Active", false);
            _isCharge = true;
        }
        else if(Time.time >= disableTime && Time.time < closeTime)
        {
            _anim.SetBool("Active", false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(Time.time >= closeTime)
        {
            _isActive = false;
            gameObject.SetActive(false);
        }
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
    }

}
