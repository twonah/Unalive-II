using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    
    public GameObject plantform;
    public Transform posA, posB;

    [SerializeField] float Speed = 0f;

    bool onMoving = false;

    Vector2 targetPos;

    private void Start()
    {
        targetPos = posB.position;
    }

    void Update()
    {
        if (onMoving == true)
        {

            if (Vector2.Distance(transform.position, posA.position) < .1f) targetPos = posB.position;
            plantform.transform.position = Vector2.MoveTowards(plantform.transform.position, targetPos, Speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onMoving = true;
            Debug.Log("TEST");

        }
    }
}

