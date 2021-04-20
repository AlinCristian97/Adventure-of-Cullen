using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int InputX;
    protected int InputY;
    private bool _jumpInput;
    private bool _grabInput;
    
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;
    
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

    public override void Execute()
    {
        base.Execute();

        InputX = Player.InputHandler.NormalizedInputX;
        InputY = Player.InputHandler.NormalizedInputY;
        _jumpInput = Player.InputHandler.JumpInput;
        _grabInput = Player.InputHandler.GrabInput;

        if (_jumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!_isGrounded)
        {
            Debug.Log("!isGrounded");
            // Player.AirState.StartCoyoteTime();
            // StateMachine.ChangeState(Player.AirState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }
}