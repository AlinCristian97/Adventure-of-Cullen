using UnityEngine;

public class PlayerActions : CharacterActions
{
    private Player _player;

    public PlayerActions(Player player)
    {
        _player = player;
    }
    
    private Vector2 GetNewVelocity() => new Vector2(CalculateHorizontalVelocity(), _player.Stats.Direction.y);
    
    private float CalculateHorizontalVelocity()
    {
        float horizontalVelocity = _player.Stats.Direction.x * _player.Stats.MovementSpeed * Time.fixedDeltaTime;
        return horizontalVelocity;
    }
    
    public override void Move(Transform transform)
    {
        _player.Components.Rigidbody.velocity = GetNewVelocity();
    }
}