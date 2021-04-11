using UnityEngine;

[System.Serializable]
public class PlayerBrain
{
    private Player _player;
    public InputMaster Input { get; }
    public Vector2 Direction { get; private set; }
    
    private LayerMask _whatIsGround;

    public PlayerBrain(Player player)
    {
        _player = player;
        _whatIsGround = LayerMask.GetMask("Ground");
        Input = new InputMaster();
        Input.Player.Jump.performed += _ => _player.Actions.TryJump();
    }

    public void HandleDecisions()
    {
        SetMovementDirection();
    }

    private void SetMovementDirection()
    {
        Direction = new Vector2(
            GetHorizontalMovementInput(),
            _player.Components.Rigidbody.velocity.y);
    }
    
    private float GetHorizontalMovementInput()
    {
        return Input.Player.Move.ReadValue<float>();
    }
    
    public bool IsGrounded()
    {
        Bounds bounds = _player.Components.Collider.bounds;
        
        RaycastHit2D hit = Physics2D.BoxCast(bounds.center, bounds
            .size, 0, Vector2.down, 0.1f, _whatIsGround);

        return hit.collider != null;
    }
}