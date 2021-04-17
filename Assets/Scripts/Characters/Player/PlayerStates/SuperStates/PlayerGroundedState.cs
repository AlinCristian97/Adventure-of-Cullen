using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int InputX;
    private bool _jumpInput;
    private bool _isGrounded;
    
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        
        Player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        InputX = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;

        if (_jumpInput && Player.JumpState.CanJump())
        {
            Player.InputHandler.UseJumpInput();
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!_isGrounded)
        {
            Player.AirState.StartCoyoteTime();
            StateMachine.ChangeState(Player.AirState);
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
}