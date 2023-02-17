using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement PM;
    [SerializeField] private HitPoints HP;
    [SerializeField] private Controll_Script SwitchControls;
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WalkAnimation();

        JumpAnimation();

        DashAnimation();

        HurtAnimation();

        DeadAnimation();

        TransformAnimation();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void WalkAnimation()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        _anim.SetFloat("Speed", Mathf.Abs(horizontal));
    }
    private void JumpAnimation()
    {
        if (PM.isPressingJump)
        {
            _anim.SetBool("IsJump", true);
        }

        bool isGrounded = IsGrounded();

        if (!PM.isPressingJump && isGrounded)
        {
            _anim.SetBool("IsJump", false);
        }
    }

    private void DashAnimation()
    {
        if(PM.isDashing)
        {
            _anim.SetBool("IsDash", true);
        }
        
        if(!PM.isDashing)
        {
            _anim.SetBool("IsDash", false);
        }
    }

    private void HurtAnimation()
    {
        if(HP._IsTakingDamage)
        {
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        _anim.SetBool("IsTakingDamage", true);
        yield return new WaitForSeconds(0.25f);
        _anim.SetBool("IsTakingDamage", false);
        HP._IsTakingDamage = false;
    }

    private void DeadAnimation()
    {
        if(HP._CurrentHitPoints <= 0)
        {
            _anim.SetTrigger("Die");
        }
    }

    private void TransformAnimation()
    {
        StartCoroutine(TransformDelay());
    }

    private IEnumerator TransformDelay()
    {
        if(SwitchControls.isDreamWalkerToDreamform)
        {
            _anim.SetBool("IsTransform", true);
            yield return new WaitForSeconds(0.5f);
            _anim.SetBool("IsTransform", false);
            _anim.SetBool("IsDreamform", true);
        }

        if (SwitchControls.isDreamWalkerToPlayer)
        {
            _anim.SetBool("IsDreamform", false);
            yield return new WaitForSeconds(0.5f);
            _anim.SetBool("IsDreamform", true);
        }
    }
}
