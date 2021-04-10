using UnityEngine;

public class PlayerBrain : CharacterBrain
{
    private Player _player;
    private InputMaster _input;

    public PlayerBrain(Player player)
    {
        _player = player;
        _input = new InputMaster();
        _input.Enable();
    }

    public override void HandleDecisions()
    {
        SetMovementDirection();
    }

    private void SetMovementDirection()
    {
        _player.Stats.Direction = new Vector2(
            GetHorizontalMovementInput(),
            _player.Components.Rigidbody.velocity.y);
    }
    
    private float GetHorizontalMovementInput()
    {
        return _input.Player.Move.ReadValue<float>();
    }
}