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

    // Start is called before the first frame update
    void Start()
    {
        _isCharge = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        StartCoroutine(LaserControl());
    }

    private IEnumerator LaserControl()
    {   
        yield return new WaitForSeconds(_chargeDuration);

        _anim.SetBool("Active", true);
        _isCharge = false;
        _isActive = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(_activeDuration);

        _anim.SetBool("Active", false);
        _isActive = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(_disableDuration);

        gameObject.SetActive(false);
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
