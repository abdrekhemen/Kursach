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
    private bool isDownPressed;

    private GameObject currentOneWayPlatform;

    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private new string tag;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float jumpPower = 16f;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isJumpPressed = isJumpPressed || Input.GetButtonDown("Jump");
        isShiftPressed = isShiftPressed || Input.GetKeyDown(KeyCode.LeftShift);
        isDownPressed = isDownPressed || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        Flip();

    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (isJumpPressed && isGrounded())
        {
            rb.gravityScale = gravityScale;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (isShiftPressed && canDash)
        {
            StartCoroutine(Dash());
        }
        isJumpPressed = false; 
        isShiftPressed = false;
        isDownPressed = false;

        if (rb.velocity.y > 0)
        {
            rb.gravityScale = gravityScale;
        }
        else
        {
            rb.gravityScale = fallGravityScale;
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

    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up*castDistance, boxSize);
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