using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D _rb;
    private bool _isFacingRight = true;
    private bool _isGrounded;

    public void Initialize(Rigidbody2D rb) => _rb = rb;

    public void Move(float moveInput)
    {
        _rb.velocity = new Vector2(moveInput * moveSpeed, _rb.velocity.y);

        if (moveInput > 0 && !_isFacingRight)
            Flip();
        else if (moveInput < 0 && _isFacingRight)
            Flip();
    }

    public void Jump()
    {
        if (_isGrounded)
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    public bool IsFacingRight() => _isFacingRight;

    public float GetMoveInput() =>  Input.GetAxis("Horizontal");

    public bool IsGrounded() => _isGrounded;

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _isGrounded = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _isGrounded = false;
    }
}
