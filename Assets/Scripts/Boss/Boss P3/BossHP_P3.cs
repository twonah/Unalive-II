using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP_P3 : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float currentHP;

    [SerializeField] float dreamFormDMG;
    [SerializeField] GameObject core;
    [SerializeField] bool isStunned;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        isStunned = false;
        core.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP <= 0)
        {
            isStunned = true;
            core.SetActive(true);
            rb.gravityScale = 100f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("DreamFormATK"))
        {
            currentHP -= dreamFormDMG;
        }
    }
}
