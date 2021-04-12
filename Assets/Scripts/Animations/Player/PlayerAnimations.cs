using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Player _player;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        HandleMovementAnimation();
        HandleJumpAnimation();
        HandleWallSlideAnimation();
    }

    private void HandleMovementAnimation()
    {
        _animator.SetBool("isRunning", _player.Brain.Direction.x != 0 && _player.Brain.Direction.y == 0);
        FlipHorizontal();
    }

    private void HandleJumpAnimation()
    {
        _animator.SetBool("isGrounded", _player.Brain.IsGrounded);
        _animator.SetFloat("yVelocity", _player.Brain.Direction.y);
    }

    private void HandleWallSlideAnimation()
    {
        _animator.SetBool("isWallSliding", _player.Brain.IsWallSliding);
    }
    
    private void FlipHorizontal()
    {
        if (_player.Brain.Direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_player.Brain.Direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}