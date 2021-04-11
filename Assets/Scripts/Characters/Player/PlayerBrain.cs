using UnityEngine;

[System.Serializable]
public class PlayerBrain
{
    private Player _player;
    public InputMaster Input { get; }
    public Vector2 Direction { get; private set; }
    private LayerMask _whatIsGround;
    public bool IsGrounded { get; private set; }

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
        CheckGrounded();
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
    
    public void CheckGrounded()
    {
        Bounds bounds = _player.Components.Collider.bounds;
        float scanSize = 0.01f;
        
        RaycastHit2D hit = Physics2D.BoxCast(bounds.center, bounds
            .size, 0, Vector2.down, scanSize, _whatIsGround);
        
        // Debug.DrawRay(bounds.center + new Vector3(bounds.extents.x, 0), Vector3.down * (bounds.extents.y + scanSize), Color.blue);
        // Debug.DrawRay(bounds.center - new Vector3(bounds.extents.x, 0), Vector3.down * (bounds.extents.y + scanSize), Color.blue);
        // Debug.DrawRay(bounds.center - new Vector3(bounds.extents.x, bounds.extents.y + scanSize), Vector3.right * (bounds.extents.x * 2), Color.blue);

        IsGrounded = hit.collider != null;
    }
}