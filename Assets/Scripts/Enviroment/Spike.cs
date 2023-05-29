using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float _knockback;
    [SerializeField] private float _damage;
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player"))
        {
            _player.GetComponent<HitPoints>().TakeDamage(_damage);

            Knockback();
        }
    }

    private void Knockback()
    {
        //_player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _knockback, ForceMode2D.Impulse);
        _player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,_knockback);
        
    }
}
