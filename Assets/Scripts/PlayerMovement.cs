using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void HandleInput()
    {
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
            _isJumping = true;
        }
        if (Input.GetButtonUp("Jump") && _rigidbody2d.velocity.y > 0)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _rigidbody2d.velocity.y * 0.5f);
            _isJumping = false;
        }
    }
}
