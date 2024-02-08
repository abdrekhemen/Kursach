using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float moveSpeed = 8f;
    private float gravityScale = 1;
    private float fallGravityScale = 3;
    
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private bool isJumpPressed;
    private bool isShiftPressed;
    private bool isGrounded;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private new string tag;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float jumpHeight = 8f;


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isJumpPressed = isJumpPressed || Input.GetButtonDown("Jump");
        isShiftPressed = isShiftPressed || Input.GetKeyDown(KeyCode.LeftShift);

        Flip();
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (isJumpPressed && isGrounded)
        {
            rb.gravityScale = gravityScale;
            float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (isShiftPressed && canDash )
        {
            StartCoroutine(Dash());
        }
        isJumpPressed = false;
        isShiftPressed = false;

        if(rb.velocity.y > 0)
        {
            rb.gravityScale = gravityScale;
        }
        else
        {
            rb.gravityScale = fallGravityScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
   void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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