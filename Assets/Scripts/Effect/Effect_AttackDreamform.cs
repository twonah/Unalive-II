using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_AttackDreamform : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private LayerMask _layer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((_layer.value & (1 << collision.transform.gameObject.layer)) != 0)
        {
            _anim.SetTrigger("Hit");
        }

    }

    public void DestroyWhenHit()    //Use in animation
    {
        Destroy(gameObject);
    }
}
