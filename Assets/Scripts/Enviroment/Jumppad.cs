using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MonoBehaviour
{
    public float bounce = 15f;
    [SerializeField] private Animator _anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bounce);
            _anim.SetFloat("Activate", 1);
        } 
    }

    void AnimEnd()  //Use this in the animation
    {
        _anim.SetFloat("Activate", -1);
    }
}
