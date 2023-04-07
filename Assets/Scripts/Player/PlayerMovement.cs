using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] public float DashingCooldown = 1f;
    private bool isFacingRight = true;
    private bool canDash = true;
    public bool isDashing;
    public bool IsCooldown = false;

    [SerializeField] float dashingPower = 24f;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 14f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private TrailRenderer tr;
    [SerializeField] private LayerMask groundLayer;

    public bool isPressingJump;     //Use in PlayerAnimator

    PostProcessVolume m_Volume;
    Vignette m_Vignette;


    private void Start()
    {

    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        bool isGrounded = IsGrounded();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isPressingJump = true;  
        }

        if (Input.GetButtonUp("Jump"))
        {
            if(rb.velocity.y > 0f)rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            isPressingJump = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash || Input.GetButtonDown("Fire1") && canDash)
        {
            StartCoroutine(Dash());
        }


        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public void StopWalking()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);  
    }

    public IEnumerator Dash()
    {
        IsCooldown = true;
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(DashingCooldown);
        IsCooldown = false;
        canDash = true;
    }

}
