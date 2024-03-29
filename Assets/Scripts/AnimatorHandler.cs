using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    private Animator _animator;
    private Movement _movement;

    private Vector3 _initialScale = Vector3.one;
    private Vector3 _flipScale = Vector3.one;



    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = transform.parent.GetComponent<Movement>();

        _initialScale = transform.localScale;
        _flipScale = new Vector3(-_initialScale.x, _initialScale.y, _initialScale.z);
    }

 
    void Update()
    {
        HandleFlip();
        UpdateAnimator();
    }

    void HandleFlip()
    {
        if (_movement == null)
            return;

        if (_movement.FlipAnim == true)
        {
            transform.localScale = _flipScale;
        }

        if (_movement.FlipAnim == false)
        {
            transform.localScale = _initialScale;
        }
    }

    void UpdateAnimator()
    {
        if (_movement == null || _animator == null)
            return;

        _animator.SetBool("isJumping", _movement.IsJumping);
        _animator.SetBool("isRunning", _movement.IsRunning);
        _animator.SetBool("isFalling", _movement.IsFalling);
        _animator.SetBool("isGrounded", _movement.IsGrounded);
    }
}
