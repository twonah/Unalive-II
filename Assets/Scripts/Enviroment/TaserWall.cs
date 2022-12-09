using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserWall : MonoBehaviour
{
    [SerializeField] private float _damage;
    public GameObject Taser;
    private void Start()
    {
        Taser.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<HitPoints>().TakeDamage(_damage);

            Debug.Log("You do dmg 1");
        }
    }
}
