using UnityEngine;

public class PlayerAirState : PlayerState
{
    private int InputX;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingWallBack;
    private bool _oldIsTouchingWall;
    private bool _oldIsTouchingWallBack;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _coyoteTime;
    private bool _wallJumpCoyoteTime;
    private bool _isJumping;
    private bool _grabInput;
    private bool _isTouchingLedge;

    private float _startWallJumpCoyoteTime;
    
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        _oldIsTouchingWall = false;
        _oldIsTouchingWallBack = false;
        _isTouchingWall = false;
        _isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        InputX = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;
        _grabInput = Player.InputHandler.GrabInput;

        CheckJumpMultiplier();

        if (_isGrounded && Player.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
        {
            StateMachine.ChangeState(Player.LedgeClimbState);
        }
        else if (_jumpInput && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            _isTouchingWall = Player.CheckIfTouchingWall();
            Player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            StateMachine.ChangeState(Player.WallJumpState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            _coyoteTime = false;
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
        else if (_isTouchingWall && InputX == Player.FacingDirection && Player.CurrentVelocity.y <= 0f)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            Player.CheckIfShouldFlip(InputX);
            Player.SetVelocityX(PlayerData.MovementVelocity * InputX);
            
            Player.Animator.SetFloat("yVelocity", Player.CurrentVelocity.y);
        }
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                Player.SetVelocityY(Player.CurrentVelocity.y * PlayerData.VariableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if (Player.CurrentVelocity.y <= 0)
            {
                _isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _oldIsTouchingWall = _isTouchingWall;
        _oldIsTouchingWallBack = _isTouchingWallBack;
        
        _isGrounded = Player.CheckIfGrounded();
        _isTouchingWall = Player.CheckIfTouchingWall();
        _isTouchingWallBack = Player.CheckIfTouchingWallBack();
        _isTouchingLedge = Player.CheckIfTouchingLedge();

        if (_isTouchingWall && !_isTouchingLedge)
        {
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
        
        if (!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBack && (_oldIsTouchingWall || _oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > StartTime + PlayerData.CoyoteTime)
        {
            _coyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (_wallJumpCoyoteTime && Time.time > _startWallJumpCoyoteTime + PlayerData.CoyoteTime)
        {
            _wallJumpCoyoteTime = false;
        }
    }
    
    public void StartCoyoteTime() => _coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = true;
        _startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;

    
    
    public void SetIsJumping() => _isJumping = true;
}