using UnityEngine;

[System.Serializable]
public class PlayerBrain
{
    private Player _player;
    public InputMaster Input { get; }
    public Vector2 Direction { get; private set; }
    private LayerMask _whatIsGround;
    public bool IsGrounded { get; private set; }
    public bool IsTouchingWall { get; private set; }
    public bool IsWallSliding { get; set; }
    [field: SerializeField] public Vector2 WallHopDirection { get; private set; }
    [field: SerializeField] public Vector2 WallJumpDirection { get; private set; }
    
    public PlayerBrain(Player player)
    {
        _player = player;
        _whatIsGround = LayerMask.GetMask("Ground");
        
        WallHopDirection = _player.Brain.WallHopDirection.normalized;
        WallJumpDirection = _player.Brain.WallJumpDirection.normalized;
        
        Input = new InputMaster();
        Input.Player.Jump.performed += _ => _player.Actions.TryJump();
    }

    public void HandleInput()
    {
        SetMovementDirection();
    }

    public void CheckSurroundings()
    {
        CheckGrounded();
        CheckWallTouch();
        CheckWallSliding();
    }

    private void CheckWallSliding()
    {
        if (IsTouchingWall && !IsGrounded && _player.Brain.Direction.y < 0)
        {
            IsWallSliding = true;
        }
        else
        {
            IsWallSliding = false;
        }
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
    
    private void CheckGrounded()
    {
        float checkDistance = 0.01f;
        float horizontalSizeReductionFactor = 0.8f;
        Bounds bounds = _player.Components.Collider.bounds;
        Vector2 boxCastSize = new Vector2(bounds.size.x * horizontalSizeReductionFactor, bounds.size.y);
        
        RaycastHit2D hit = Physics2D.BoxCast(bounds.center, boxCastSize, 0,
            Vector2.down, checkDistance, _whatIsGround);
        
        // //Debug
        // Debug.DrawRay(
        //     bounds.center + new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0),
        //     Vector3.down * (bounds.extents.y + checkDistance),
        //     Color.blue);
        //
        // Debug.DrawRay(
        //     bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0), 
        //     Vector3.down * (bounds.extents.y + checkDistance),
        //     Color.blue);
        //
        // Debug.DrawRay(
        //     bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor, bounds.extents.y + checkDistance),
        //     Vector3.right * (bounds.extents.x),
        //     Color.blue);

        IsGrounded = hit.collider != null;
    }

    private void CheckWallTouch()
    {
        float checkDistance = 0.01f;
        Bounds bounds = _player.Components.Collider.bounds;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(bounds.min.x - checkDistance, bounds.center.y), Vector2.right,
            bounds.size.x + checkDistance * 2, _whatIsGround);
        
        
        // //Debug
        Debug.DrawRay(new Vector2(bounds.min.x - checkDistance, bounds.center.y), new Vector2(bounds.size.x + checkDistance * 2, 0), Color.yellow);

        IsTouchingWall = hit.collider != null;
    }
}