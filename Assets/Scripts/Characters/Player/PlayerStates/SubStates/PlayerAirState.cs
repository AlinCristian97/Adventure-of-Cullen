using UnityEngine;

public class PlayerAirState : PlayerState
{
    private int InputX;
    private bool _isGrounded;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _coyoteTime;
    private bool _isJumping;
    
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        CheckCoyoteTime();

        InputX = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;

        CheckJumpMultiplier();

        if (_isGrounded && Player.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
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

        _isGrounded = Player.CheckIfGrounded();
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > StartTime + PlayerData.CoyoteTime)
        {
            _coyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void SetIsJumping() => _isJumping = true;
}