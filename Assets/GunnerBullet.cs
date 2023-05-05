using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBullet : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 0f;
    [SerializeField] private float bulletDamage = 0f;

    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string dreamformTag = "DreamForm";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        Destroy(gameObject, destroyDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(playerTag))
        {
            HitPoints playerHP = collision.gameObject.GetComponent<HitPoints>();

            if (playerHP != null)
            {
                playerHP.TakeDamage(bulletDamage);
            }
        }

        if (collision.gameObject.CompareTag(dreamformTag))
        {
            HitPoints dreamFormHP = collision.gameObject.GetComponent<HitPoints>();

            if (dreamFormHP != null)
            {
                dreamFormHP.TakeDamage(bulletDamage);
            }
        }

        Destroy(gameObject);
    }
}
