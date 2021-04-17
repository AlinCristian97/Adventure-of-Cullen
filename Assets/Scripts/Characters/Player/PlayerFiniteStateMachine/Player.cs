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
    // public PlayerLandState LandState { get; private set; }
    [SerializeField] private PlayerData _playerData;

    //test
    public bool testWallTouch;
    public bool testGrounded;

    #endregion

    #region Components

    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Collider2D Collider { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    #endregion

    #region Check Transforms

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _wallCheck;
    
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
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

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
        
        // //Debug
        // Debug.DrawRay(
        //     bounds.center + new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0),
        //     Vector3.down * (bounds.extents.y + _playerData.GroundCheckDistance),
        //     Color.blue);
        //
        // Debug.DrawRay(
        //     bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0), 
        //     Vector3.down * (bounds.extents.y + _playerData.GroundCheckDistance),
        //     Color.blue);
        //
        // Debug.DrawRay(
        //     bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor,
        //         bounds.extents.y + _playerData.GroundCheckDistance),
        //     Vector3.right * (bounds.size.x * horizontalSizeReductionFactor),
        //     Color.blue);

        //test
        testGrounded = hit;
        
        return hit;
    }

    public bool CheckIfTouchingWall()
    {
        Bounds bounds = Collider.bounds;

        RaycastHit2D hit = Physics2D.Raycast(bounds.center, Vector2.right * FacingDirection,
            bounds.extents.x + _playerData.WallCheckDistance, _playerData.WhatIsGround);
        
        // //Debug
        // Debug.DrawRay(bounds.center,
        //     new Vector2((bounds.extents.x + _playerData.WallCheckDistance) * FacingDirection, 0),
        //     Color.yellow);

        //test
        testWallTouch = hit;
        
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

    #endregion
}