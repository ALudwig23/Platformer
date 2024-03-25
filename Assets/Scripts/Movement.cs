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

    public Cooldown CoyoteTime;
    public Cooldown BufferJump;

    public LayerMask GroundLayerMask;

    protected bool _isGrounded = false;
    protected bool _isJumping = false;
    protected bool _isFalling = false;
    protected bool _isRunning = false;
    protected bool _canJump = true;
    protected bool _preJumpInput = false;
    protected bool _isNearGround = false;

    protected Vector2 _inputDirection;

    protected Rigidbody2D _rigidbody2d;
    protected Collider2D _collider2d;

    public bool IsRunning { get { return _isRunning; } }
    public bool IsJumping { get { return _isJumping; } }
    public bool IsFalling { get { return _isFalling; } }
    public bool IsGrounded { get { return _isGrounded; } }
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
        TryBuffer();
    }

    protected virtual void HandleInput()
    {
       
    }

    protected virtual void TryBuffer()
    {
        _isNearGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius*10, GroundLayerMask);

        if (_rigidbody2d.velocity.y < 0 && !_isGrounded)
        {
            BufferJump.StartCooldown();


            if (Input.GetButton("Jump"))
            {
                Debug.Log("Jump Buffering");
                _preJumpInput = true;
            }
        }

        if (_isGrounded)
        {
            BufferJump.StopCooldown();
        }


        if (_isGrounded && _preJumpInput == true)
        {
            Jump();
            _preJumpInput = false;
        }
    }

    protected virtual void Jump()
    {
        if (!_canJump)
            return;

        if (CoyoteTime.CurrentProgress == Cooldown.Progress.Finished)
            return;

        _canJump = false;
        _isJumping = true;


        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, jumpForce);
        //Debug.Log("Holding Jump");
        
        CoyoteTime.StopCooldown();
        //Debug.Log("Jumping");
    }
    protected virtual void HandleMovement()
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

        if (TargetVelocity.x == 0)
        {
            _isRunning = false;
        }
        else
        {
            _isRunning = true;
        }
    }

    void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayerMask);
        //Debug.Log("Grounded" + _isGrounded);

        if(_rigidbody2d.velocity.y <= 0)
        {
            _isJumping = false;
            _isFalling = false;
        }

        if (_isGrounded && !IsJumping)
        {
            _canJump = true;
            if (CoyoteTime.CurrentProgress != Cooldown.Progress.Ready)
                CoyoteTime.StopCooldown();
            
        }

        if (!_isGrounded && !_isJumping && CoyoteTime.CurrentProgress == Cooldown.Progress.Ready)
            CoyoteTime.StartCooldown();
    }
}
