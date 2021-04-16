using StrategyPattern.Behaviours;
using StrategyPattern.Interfaces;
using UnityEngine;

[System.Serializable]
public class PlayerActions
{
    private PlayerOld _playerOld;
    private IJumpBehaviour _jumpBehaviour;

    public PlayerActions(PlayerOld playerOld)
    {
        _playerOld = playerOld;
        _jumpBehaviour = new NormalJump();
    }
    
    public PlayerActions(PlayerOld playerOld, IJumpBehaviour jumpBehaviour) : this(playerOld)
    {
        _jumpBehaviour = jumpBehaviour;
    }

    private Vector2 GetNewVelocity() => new Vector2(CalculateHorizontalVelocity(), _playerOld.Brain.Direction.y);
    
    private float CalculateHorizontalVelocity()
    {
        float horizontalVelocity = _playerOld.Brain.Direction.x * _playerOld.Stats.MovementSpeed * Time.fixedDeltaTime;
        return horizontalVelocity;
    }
    
    public void Move()
    {
        if (!_playerOld.Brain.IsWallSliding)
        {
            _playerOld.Components.Rigidbody.velocity = GetNewVelocity();
        }
    }

    public void HandleWallSlide()
    {
        if (_playerOld.Brain.IsWallSliding)
        {
            // if (_player.Components.Rigidbody.velocity.y < -_player.Stats.WallSlideSpeed)
            // {
                _playerOld.Components.Rigidbody.velocity = new Vector2(
                    _playerOld.Components.Rigidbody.velocity.x,
                    -_playerOld.Stats.WallSlideSpeed);
            // }
        }
    }

    public void TryJump()
    {
        if (_playerOld.Brain.IsGrounded && !_playerOld.Brain.IsWallSliding)
        {
            NormalJump();
        }
        
        else if ((_playerOld.Brain.IsWallSliding || _playerOld.Brain.IsTouchingWall))
        {
            WallJump();
        }
    }

    public void NormalJump()
    {
        _jumpBehaviour.Jump(_playerOld.Components.Rigidbody, _playerOld.Stats.JumpForce);
    }

    public void WallJump()
    {
        _playerOld.Brain.IsWallSliding = false;
        Vector2 forceToAdd = new Vector2(_playerOld.Brain.Direction.x, _playerOld.Brain.WallJumpDirection.y)
                             * _playerOld.Stats.WallJumpForce;
            
        _playerOld.Components.Rigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);
    }
}