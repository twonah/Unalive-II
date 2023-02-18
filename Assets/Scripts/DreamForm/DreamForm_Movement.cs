using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamForm_Movement : MonoBehaviour
{

    private float horizontal;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private bool isFacingRight = true;
    private bool canDash = true;
    public bool isDashingg;


    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpingPower = 14f;

    //private bool canSpirit = true; // detects if Dreamform can use spirit form or not


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;



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

        /*
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        */


        if (Input.GetButton("Down"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpingPower);
            changingElevation = true;
        }

        if (Input.GetButtonUp("Down"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash)
        {
            StartCoroutine(Dash());
            Debug.Log("Dashing");
        }

        if (!changingElevation)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // allows walking for dreamform
        if (isDashingg)
        {
            return;
        }
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
        canDash = false;
        isDashingg = true;
        //float originalGravity = rb.gravityScale;
        //rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        //rb.gravityScale = originalGravity;
        isDashingg = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void SpiritForm()
    {

    }


}
