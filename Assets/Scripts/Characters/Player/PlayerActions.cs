using StrategyPattern.Behaviours;
using StrategyPattern.Interfaces;
using UnityEngine;

[System.Serializable]
public class PlayerActions
{
    private Player _player;
    private IJumpBehaviour _jumpBehaviour;

    public PlayerActions(Player player)
    {
        _player = player;
        _jumpBehaviour = new NormalJump();
    }
    
    public PlayerActions(Player player, IJumpBehaviour jumpBehaviour) : this(player)
    {
        _jumpBehaviour = jumpBehaviour;
    }

    private Vector2 GetNewVelocity() => new Vector2(CalculateHorizontalVelocity(), _player.Brain.Direction.y);
    
    private float CalculateHorizontalVelocity()
    {
        float horizontalVelocity = _player.Brain.Direction.x * _player.Stats.MovementSpeed * Time.fixedDeltaTime;
        return horizontalVelocity;
    }
    
    public void Move()
    {
        if (!_player.Brain.IsWallSliding)
        {
            _player.Components.Rigidbody.velocity = GetNewVelocity();
        }
    }

    public void HandleWallSlide()
    {
        if (_player.Brain.IsWallSliding)
        {
            // if (_player.Components.Rigidbody.velocity.y < -_player.Stats.WallSlideSpeed)
            // {
                _player.Components.Rigidbody.velocity = new Vector2(
                    _player.Components.Rigidbody.velocity.x,
                    -_player.Stats.WallSlideSpeed);
            // }
        }
    }

    public void TryJump()
    {
        if (_player.Brain.IsGrounded && !_player.Brain.IsWallSliding)
        {
            NormalJump();
        }
        
        else if ((_player.Brain.IsWallSliding || _player.Brain.IsTouchingWall))
        {
            WallJump();
        }
    }

    public void NormalJump()
    {
        _jumpBehaviour.Jump(_player.Components.Rigidbody, _player.Stats.JumpForce);
    }

    public void WallJump()
    {
        _player.Brain.IsWallSliding = false;
        Vector2 forceToAdd = new Vector2(_player.Brain.Direction.x, _player.Brain.WallJumpDirection.y)
                             * _player.Stats.WallJumpForce;
            
        _player.Components.Rigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);
    }
}