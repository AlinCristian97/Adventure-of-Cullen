using UnityEngine;

public class Player : MonoBehaviour
{
    
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    // public PlayerLandState LandState { get; private set; }
    [SerializeField] private PlayerData _playerData;

    //test
    public bool testWallTouch;
    public bool testGrounded;
    public bool testLedgeTouch;
    public bool testCeiling;
    public bool testLedgeCeiling;

    #endregion

    #region Components

    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CapsuleCollider2D Collider { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    
    #endregion

    #region Other Variables

    private Vector2 _workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, _playerData, "air");
        AirState = new PlayerAirState(this, StateMachine, _playerData, "air");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, _playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, _playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, _playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, _playerData, "air");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, _playerData, "ledgeClimbState");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, _playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, _playerData, "crouchMove");
        // LandState = new PlayerLandState(this, StateMachine, _playerData, "land");
        
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CapsuleCollider2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        FacingDirection = 1;
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;
        StateMachine.CurrentState.Execute();
        
        //test
        // CheckIfTouchingLedge();
        // CheckIfTouchingWall();
        // CheckIfGrounded();
        testCeiling = CheckForCeiling();
        // CheckForCeiling();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.ExecutePhysics();
    }

    #endregion

    #region Set Functions

    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace; // Can be removed?
    }
    
    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace; // Can be removed?
    }

    public void SetVelocityY(float velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace; // Can be removed?
    }

    #endregion

    #region Check Functions

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        float horizontalSizeReductionFactor = 0.8f;
        Bounds bounds = Collider.bounds;
        Vector2 boxCastSize = new Vector2(bounds.size.x * horizontalSizeReductionFactor, bounds.size.y);
        
        RaycastHit2D hit = Physics2D.BoxCast(bounds.center, boxCastSize, 0,
            Vector2.down, _playerData.GroundCheckDistance, _playerData.WhatIsGround);
        
        //Debug
        Debug.DrawRay(
            bounds.center + new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0),
            Vector3.down * (bounds.extents.y + _playerData.GroundCheckDistance),
            Color.blue);
        
        Debug.DrawRay(
            bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0), 
            Vector3.down * (bounds.extents.y + _playerData.GroundCheckDistance),
            Color.blue);
        
        Debug.DrawRay(
            bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor,
                bounds.extents.y + _playerData.GroundCheckDistance),
            Vector3.right * (bounds.size.x * horizontalSizeReductionFactor),
            Color.blue);

        //test
        testGrounded = hit;
        
        return hit;
    }
    
    public bool CheckForCeiling()
    {
        //TODO: Improve code! Why is it /4 for debug?
        float horizontalSizeReductionFactor = 0.8f;
        Bounds bounds = Collider.bounds;
        Vector2 boxCastSize = new Vector2(bounds.size.x * horizontalSizeReductionFactor, bounds.size.y / 2);
        
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(bounds.center.x, bounds.max.y), boxCastSize, 0,
            Vector2.up, _playerData.GroundCheckDistance, _playerData.WhatIsGround);
        
        // //Debug
        // Debug.DrawRay(
        //     new Vector3(bounds.center.x, bounds.max.y) + new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0),
        //     Vector2.up * (bounds.size.y / 4 + _playerData.GroundCheckDistance),
        //     Color.red);
        //
        // Debug.DrawRay(
        //     new Vector3(bounds.center.x, bounds.max.y) - new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0), 
        //     Vector2.up * (bounds.size.y / 4 + _playerData.GroundCheckDistance),
        //     Color.red);
        //
        // Debug.DrawRay(
        //     new Vector3(bounds.center.x, bounds.max.y) + new Vector3(bounds.extents.x * horizontalSizeReductionFactor,
        //         bounds.size.y / 4 + _playerData.GroundCheckDistance),
        //     Vector2.left * (bounds.size.x * horizontalSizeReductionFactor),
        //     Color.red);
        
        return hit;
    }

    

    //TODO: Check Touching Wall as BoxCast instead of Raycast?
    public bool CheckIfTouchingWall()
    {
        Bounds bounds = Collider.bounds;

        RaycastHit2D hit = Physics2D.Raycast(bounds.center, Vector2.right * FacingDirection,
            bounds.extents.x + _playerData.WallCheckDistance, _playerData.WhatIsGround);
        
        //Debug
        // Debug.DrawRay(bounds.center,
        //     new Vector2((bounds.extents.x + _playerData.WallCheckDistance) * FacingDirection, 0),
        //     Color.yellow);

        //test
        testWallTouch = hit;
        
        return hit;
    }

    public bool CheckIfTouchingWallBack()
    {
        Bounds bounds = Collider.bounds;

        RaycastHit2D hit = Physics2D.Raycast(bounds.center, Vector2.right * -FacingDirection,
            bounds.extents.x + _playerData.WallCheckDistance, _playerData.WhatIsGround);
        
        // //Debug
        // Debug.DrawRay(bounds.center,
        //     new Vector2((bounds.extents.x + _playerData.WallCheckDistance) * -FacingDirection, 0),
        //     Color.magenta);

        //test
        testWallTouch = hit;
        
        return hit;
    }

    public bool CheckIfTouchingLedge()
    {
        float offset = 1.5f;
        Bounds bounds = Collider.bounds;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y / offset), Vector2.right * FacingDirection,
            bounds.extents.x + _playerData.WallCheckDistance, _playerData.WhatIsGround);
        
        //Debug
        // Debug.DrawRay(new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y / offset),
        //     new Vector2((bounds.extents.x + _playerData.WallCheckDistance) * FacingDirection, 0),
        //     Color.red);

        //test
        testLedgeTouch = hit;
        
        return hit;
    }

    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishedTrigger();
    
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public Vector2 DetermineCornerPosition()
    {
        float offset = 1.5f;
        Bounds bounds = Collider.bounds;

        RaycastHit2D xHit = Physics2D.Raycast(bounds.center, Vector2.right * FacingDirection,
            _playerData.WallCheckDistance, _playerData.WhatIsGround);
        float xDistance = xHit.distance;
        _workspace.Set((xDistance + 0.015f) * FacingDirection, 0f);

        RaycastHit2D yHit =
            Physics2D.Raycast(new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y / offset) + _workspace, Vector2.down,
                bounds.center.y - (bounds.center.y + bounds.extents.y / 2) + 0.015f, _playerData.WhatIsGround);
        float yDistance = yHit.distance;

        _workspace.Set(bounds.center.x + (xDistance * FacingDirection), (bounds.center.y + bounds.extents.y / offset) - yDistance);

        return _workspace;
    }

    public void SetColliderHeight(float height)
    {
        Vector2 center = Collider.offset;
        _workspace.Set(Collider.size.x, height);

        center.y += (height - Collider.size.y) / 2;
        
        Collider.size = _workspace;
        Collider.offset = center;
    }

    #endregion
}