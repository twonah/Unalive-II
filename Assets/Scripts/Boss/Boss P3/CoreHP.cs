using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreHP : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] public float currentHP;

    [SerializeField] float dreamFormDMG;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DreamFormATK"))
        {
            currentHP -= dreamFormDMG;
        }
    }
}
