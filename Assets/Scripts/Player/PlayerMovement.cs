using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    private float horizontal;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;
    private bool isFacingRight = true;
    private bool canDash = true;
    private bool isDashing;

    [SerializeField] float dashingPower = 24f;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 14f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private TrailRenderer tr;
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (isDashing)
        {
            animator.SetBool("IsDash", false);
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash || Input.GetButtonDown("Fire1") && canDash)
        {
            StartCoroutine(Dash());
            animator.SetBool("IsDash", true);
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

    private IEnumerator Dash()
    {
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

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
