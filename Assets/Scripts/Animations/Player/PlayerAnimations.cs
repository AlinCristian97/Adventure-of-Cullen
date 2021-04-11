using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Player _player;
    private SpriteRenderer _spriteRenderer;
    public static UnityEvent OnPlayerJump { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        OnPlayerJump = new UnityEvent();
        OnPlayerJump.AddListener(PlayJumpAnimation);
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        MoveAnimation();
    }

    private void MoveAnimation()
    {
        if (_player.Brain.Direction.x != 0)
        {
            PlayRunAnimation();
            FlipHorizontal();
        }
        else if (_player.Components.Rigidbody.velocity == Vector2.zero)
        {
            PlayIdleAnimation();
        }
    }

    private void PlayRunAnimation()
    {
        _player.Components.Animator.TryPlayAnimation("Run");
    }

    private void PlayIdleAnimation()
    {
        _player.Components.Animator.TryPlayAnimation("Idle");
    }

    private void PlayJumpAnimation()
    {
        _player.Components.Animator.TryPlayAnimation("Jump");
    }
    
    private void FlipHorizontal()
    {
        _spriteRenderer.flipX = _player.Brain.Direction.x < 0;
    }
}