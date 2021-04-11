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
        _jumpBehaviour = new HumanJump();
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
        _player.Components.Rigidbody.velocity = GetNewVelocity();
    }

    public void TryJump()
    {
        if (_player.Brain.IsGrounded)
        {
            _jumpBehaviour.Jump(_player.Components.Rigidbody, _player.Stats.JumpForce);
        }
    }
}