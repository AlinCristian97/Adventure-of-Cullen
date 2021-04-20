// using System;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// [RequireComponent(typeof(Animator))]
// public class PlayerAnimations : MonoBehaviour
// {
//     [FormerlySerializedAs("_player")] [SerializeField] private PlayerOld _playerOld;
//     private SpriteRenderer _spriteRenderer;
//     private Animator _animator;
//
//     private void Awake()
//     {
//         _spriteRenderer = GetComponent<SpriteRenderer>();
//         _animator = GetComponent<Animator>();
//     }
//
//     private void Update()
//     {
//         UpdateAnimations();
//     }
//
//     private void UpdateAnimations()
//     {
//         HandleMovementAnimation();
//         HandleJumpAnimation();
//         HandleWallSlideAnimation();
//     }
//
//     private void HandleMovementAnimation()
//     {
//         _animator.SetBool("isRunning", _playerOld.Brain.Direction.x != 0 && _playerOld.Brain.Direction.y == 0);
//         FlipHorizontal();
//     }
//
//     private void HandleJumpAnimation()
//     {
//         _animator.SetBool("isGrounded", _playerOld.Brain.IsGrounded);
//         _animator.SetFloat("yVelocity", _playerOld.Brain.Direction.y);
//     }
//
//     private void HandleWallSlideAnimation()
//     {
//         _animator.SetBool("isWallSliding", _playerOld.Brain.IsWallSliding);
//     }
//     
//     private void FlipHorizontal()
//     {
//         if (!_playerOld.Brain.IsWallSliding)
//         {
//             if (_playerOld.Brain.Direction.x < 0)
//             {
//                 _spriteRenderer.flipX = true;
//             }
//             else if (_playerOld.Brain.Direction.x > 0)
//             {
//                 _spriteRenderer.flipX = false;
//             }
//         }
//     }
// }