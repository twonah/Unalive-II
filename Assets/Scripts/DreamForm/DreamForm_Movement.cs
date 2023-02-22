using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamForm_Movement : MonoBehaviour
{

    private float horizontal;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] public float DashingCooldown = 1f;
    private bool isFacingRight = true;
    private bool canDash = true;
    public bool isDashingg;
    public bool IsCooldown = false;


    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpingPower = 14f;

    //private bool canSpirit = true; // detects if Dreamform can use spirit form or not


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D dreamFormCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private float wallCheckRange;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private TrailRenderer tr;

    private void Awake()
    {
        rb.velocity = new Vector2(0,0);
    }



    // Update is called once per frame
    void Update()
    {
        bool changingElevation = false;

        if (isDashingg)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");


        if (Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            changingElevation = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        }


        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }



        if (Input.GetButton("Down"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpingPower);
            changingElevation = true;
        }

        if (Input.GetButtonUp("Down"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash)        //Dashhhh
        {
            StartCoroutine(Dash());
        }

        if (!changingElevation)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        }

        Flip();

        WallCheck();
    }

    private void FixedUpdate()
    {
        if (isDashingg) 
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // allows walking for dreamform
        
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
    private IEnumerator Dash()
    {
        IsCooldown = true;
        canDash = false;
        isDashingg = true;
        dreamFormCollider.enabled = false;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //rb.gravityScale = originalGravity;
        dreamFormCollider.enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        tr.emitting = false;
        isDashingg = false;
        yield return new WaitForSeconds(DashingCooldown);
        IsCooldown = false;
        canDash = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void WallCheck()
    {
        if (Physics2D.OverlapCircle(wallCheckPoint.position, wallCheckRange, wallLayer))
        {
            dreamFormCollider.enabled = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(wallCheckPoint.position, wallCheckRange);
    }


}
