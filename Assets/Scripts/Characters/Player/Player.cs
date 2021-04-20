using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public PlayerComponents Components { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerStates States { get; private set; }
    
    [field: SerializeField] public PlayerData Data { get; private set; }
    
    #region Debug Variables

    public bool testGrabInput;
    public bool testJumpInput;
    public bool testIsGrounded;
    public bool testIsTouchingWall;
    public bool testIsTouchingLedge;
    public bool testIsTouchingCeiling;
    
    public string testCurrentState;

    #endregion
    
    //TODO: Where to put them? Player.Utilities? Also create Player.Setters and Player.CheckFunctions files?
    #region Other Variables

    private Vector2 _workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        Components = new PlayerComponents(
            GetComponent<Rigidbody2D>(),
            GetComponent<CapsuleCollider2D>(), //TODO: Don't force capsule collider
            GetComponent<Animator>());
        
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine = new PlayerStateMachine();
        States = new PlayerStates(this);
    }


    private void Start()
    {
        StateMachine.Initialize(States.IdleState);
        FacingDirection = 1;
    }

    private void Update()
    {
        CurrentVelocity = Components.Rigidbody.velocity;
        StateMachine.CurrentState.Execute();
        
        // Test
        testGrabInput = InputHandler.GrabInput;
        testJumpInput = InputHandler.JumpInput;
        testIsGrounded = CheckIfGrounded();
        testIsTouchingWall = CheckIfTouchingWall();
        testIsTouchingLedge = CheckIfTouchingLedge();
        testIsTouchingCeiling = CheckForCeiling();
        testCurrentState = StateMachine.CurrentState.ToString();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.ExecutePhysics();
    }

    #endregion

    #region Set Functions

    public void SetVelocityZero()
    {
        Components.Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Components.Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace; // Can be removed?
    }
    
    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        Components.Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace; // Can be removed?
    }

    public void SetVelocityY(float velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        Components.Rigidbody.velocity = _workspace;
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

    public bool CheckIfGrounded() //TODO: Improve to start at the bottom of the collider
    {
        float horizontalSizeReductionFactor = 0.8f;
        Bounds bounds = Components.Collider.bounds;
        Vector2 boxCastSize = new Vector2(bounds.size.x * horizontalSizeReductionFactor, bounds.size.y);
        
        RaycastHit2D hit = Physics2D.BoxCast(bounds.center, boxCastSize, 0,
            Vector2.down, Data.GroundCheckDistance, Data.WhatIsGround);
        
        //Debug
        Debug.DrawRay(
            bounds.center + new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0),
            Vector3.down * (bounds.extents.y + Data.GroundCheckDistance),
            Color.blue);
        
        Debug.DrawRay(
            bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0), 
            Vector3.down * (bounds.extents.y + Data.GroundCheckDistance),
            Color.blue);
        
        Debug.DrawRay(
            bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor,
                bounds.extents.y + Data.GroundCheckDistance),
            Vector3.right * (bounds.size.x * horizontalSizeReductionFactor),
            Color.blue);
        
        return hit;
    }
    
    public bool CheckForCeiling()
    {
        //TODO: Improve code Separate debug into a separate function! Why is it /4 for debug?
        float horizontalSizeReductionFactor = 0.8f;
        Bounds bounds = Components.Collider.bounds;
        Vector2 boxCastSize = new Vector2(bounds.size.x * horizontalSizeReductionFactor, bounds.size.y / 2);
        
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(bounds.center.x, bounds.max.y), boxCastSize, 0,
            Vector2.up, Data.GroundCheckDistance, Data.WhatIsGround);
        
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
        //TODO: Improve code Separate debug into a separate function
        Bounds bounds = Components.Collider.bounds;

        RaycastHit2D hit = Physics2D.Raycast(bounds.center, Vector2.right * FacingDirection,
            bounds.extents.x + Data.WallCheckDistance, Data.WhatIsGround);
        
        //Debug
        // Debug.DrawRay(bounds.center,
        //     new Vector2((bounds.extents.x + _playerData.WallCheckDistance) * FacingDirection, 0),
        //     Color.yellow);
        
        return hit;
    }

    public bool CheckIfTouchingLedge()
    {
        //TODO: Improve code Separate debug into a separate function
        float offset = 1.5f;
        Bounds bounds = Components.Collider.bounds;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y / offset), Vector2.right * FacingDirection,
            bounds.extents.x + Data.WallCheckDistance, Data.WhatIsGround);
        
        //Debug
        // Debug.DrawRay(new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y / offset),
        //     new Vector2((bounds.extents.x + _playerData.WallCheckDistance) * FacingDirection, 0),
        //     Color.red);
        
        return hit;
    }

    #endregion

    #region Other Functions

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public Vector2 DetermineCornerPosition()
    {
        //TODO: Improve code readability
        float offset = 1.5f;
        Bounds bounds = Components.Collider.bounds;

        RaycastHit2D xHit = Physics2D.Raycast(bounds.center, Vector2.right * FacingDirection,
            Data.WallCheckDistance, Data.WhatIsGround);
        float xDistance = xHit.distance;
        _workspace.Set((xDistance + 0.015f) * FacingDirection, 0f);

        RaycastHit2D yHit =
            Physics2D.Raycast(new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y / offset) + _workspace, Vector2.down,
                bounds.center.y - (bounds.center.y + bounds.extents.y / 2) + 0.015f, Data.WhatIsGround);
        float yDistance = yHit.distance;

        _workspace.Set(bounds.center.x + (xDistance * FacingDirection), (bounds.center.y + bounds.extents.y / offset) - yDistance);

        return _workspace;
    }

    public void SetColliderHeight(float height)
    {
        //TODO: Improve code readability

        Vector2 center = Components.Collider.offset;
        _workspace.Set(Components.Collider.size.x, height);

        center.y += (height - Components.Collider.size.y) / 2;
        
        Components.Collider.size = _workspace;
        Components.Collider.offset = center;
    }

    #endregion
}