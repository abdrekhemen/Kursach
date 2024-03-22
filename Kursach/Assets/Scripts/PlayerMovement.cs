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
    private bool isDashing;
    private bool isJumpPressed;
    private bool isDownPressed;

    private GameObject currentOneWayPlatform;

    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private new string tag;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpPower = 16f;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        isJumpPressed = isJumpPressed || Input.GetButtonDown("Jump");
        isDownPressed = isDownPressed || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        Flip();

    }
    private void FixedUpdate()
    {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (isJumpPressed && isGrounded())
        {
            rb.gravityScale = gravityScale;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            animator.SetBool("IsJumping", true);
        }
        else if (!isJumpPressed && isGrounded())
        {
            animator.SetBool("IsJumping", false);
        }

        if(isDownPressed)
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }

        isJumpPressed = false; 
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            currentOneWayPlatform = null;
        }
    }
    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}