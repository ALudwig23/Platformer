using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float accelerator = 10f;
    public float jumpForce = 7f;

    //check ground
    public Transform GroundCheck;
    public float GroundCheckRadius = 1f;
    public float MaxSlopeAngle = 45f;

    public LayerMask GroundLayerMask;

    protected bool _isGrounded = false;
    protected bool _isJumping = false;

    protected Vector2 _inputDirection;

    protected Rigidbody2D _rigidbody2d;
    protected Collider2D _collider2d;

    void Start()
    {
        //cache for later use
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _collider2d = GetComponent<Collider2D>();
    }

    protected void Update()
    {
        HandleInput();
    }

    protected void FixedUpdate()
    {
        CheckGround();
        HandleMovement();
    }

    protected virtual void HandleInput()
    {
       
    }

    void Jump()
    {
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpForce);
        Debug.Log("Jumping");
    }
    void HandleMovement()
    {
        Vector3 TargetVelocity = Vector3.zero;
        if (_isGrounded && !_isJumping)
        {
            TargetVelocity = new Vector2(_inputDirection.x * (accelerator), 0f);
        }
        else
        {
            TargetVelocity = new Vector2(_inputDirection.x * (accelerator), _rigidbody2d.velocity.y);
        }
        
        _rigidbody2d.velocity = TargetVelocity;
    }

    void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayerMask);
        Debug.Log("Grounded" + _isGrounded);
        if (_isGrounded)
        {
            if (_isJumping)
            {
                Jump();
            }
        }
        
    }
}
