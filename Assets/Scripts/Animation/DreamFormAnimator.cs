using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamFormAnimator : MonoBehaviour
{
    [SerializeField] private Controll_Script CS;
    [SerializeField] private DreamForm_Movement DM;
    [SerializeField] private DreamForm_Punch DP;
    [SerializeField] private Animator _anim;

    private float horizontal;
    private bool isIdle;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(CS.isDreamWalker)    //Be Dreamform
        {
            //_anim.SetBool("IsActivateDreamForm", true); //
            //_anim.SetBool("IsDisableDreamForm", false);
            //_anim.SetTrigger("Appear");
            //_anim.SetBool("IsDreamForm", false);


        }
        if(!CS.isDreamWalker)   //Be player
        {
            //_anim.SetBool("IsActivateDreamForm", false); //
            //_anim.SetBool("IsDisableDreamForm", true);
            //_anim.SetTrigger("Disappear");
            //_anim.SetBool("IsDreamForm", true);

        }
        
        horizontal = Input.GetAxisRaw("Horizontal");

        _anim.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    void NotActivateDreamForm()
    {
        _anim.SetBool("IsActivateDreamForm", false);
    }

    void BeDreamForm()
    {
        _anim.SetBool("IsDreamForm", true);
    }

    void NotDreamForm()
    {
        _anim.SetBool("IsDreamForm", false);
    }

    void AnimationEndTrue()
    {
        _anim.SetBool("AnimationEnd", true);
    }

    void AnimationEndFalse()
    {
        _anim.SetBool("AnimationEnd", false);
    }
}
