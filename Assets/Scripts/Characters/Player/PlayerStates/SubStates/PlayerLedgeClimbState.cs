using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;

    private bool _isHanging;
    private bool _isClimbing;
    private bool _jumpInput;
    private bool _isTouchingCeiling;

    private int InputX;
    private int InputY;
    
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;
   
    public override void Enter()
    {
        base.Enter();
        
        Player.SetVelocityZero();
        Player.transform.position = _detectedPosition;
        _cornerPosition = Player.DetermineCornerPosition();
        
        _startPosition.Set(_cornerPosition.x - (Player.FacingDirection * PlayerData.StartOffset.x),
            _cornerPosition.y - PlayerData.StartOffset.y);
        
        _stopPosition.Set(_cornerPosition.x + (Player.FacingDirection * PlayerData.StopOffset.x),
            _cornerPosition.y + PlayerData.StopOffset.y);

        Player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        _isHanging = false;

        if (_isClimbing)
        {
            Player.transform.position = _stopPosition;
            _isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    
        if (IsAnimationFinished)
        {
            if (_isTouchingCeiling)
            {
                StateMachine.ChangeState(Player.CrouchIdleState);
            }
            else
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
        else
        {
            InputX = Player.InputHandler.NormalizedInputX;
            InputY = Player.InputHandler.NormalizedInputY;
            _jumpInput = Player.InputHandler.JumpInput;
        
            Player.SetVelocityZero();
            Player.transform.position = _startPosition;

            if (InputX == Player.FacingDirection && _isHanging && !_isClimbing)
            {
                CheckForSpace();
                _isClimbing = true;
                Player.Animator.SetBool("climbLedge", true);
            }
            else if (InputY == -1 && _isHanging && !_isClimbing)
            {
                StateMachine.ChangeState(Player.AirState);
            }
            else if (_jumpInput && !_isClimbing)
            {
                Player.WallJumpState.DetermineWallJumpDirection(true);
                StateMachine.ChangeState(Player.WallJumpState);
            }
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isHanging = true;
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        
        Player.Animator.SetBool("climbLedge", false);
    }

    private void CheckForSpace()
    {
        _isTouchingCeiling = Physics2D.Raycast(_cornerPosition + (Vector2.up * 0.015f)
                                                               + (Vector2.right * (Player.FacingDirection * 0.35f)),
            Vector2.up, PlayerData.StandColliderHeight, PlayerData.WhatIsGround);
        
        //Debug
        Debug.DrawRay(_cornerPosition + (Vector2.up * 0.015f)
                                      + (Vector2.right * (Player.FacingDirection * 0.35f)), Vector2.up * PlayerData.StandColliderHeight, Color.magenta);

        Player.testLedgeCeiling = _isTouchingCeiling;
    }
}