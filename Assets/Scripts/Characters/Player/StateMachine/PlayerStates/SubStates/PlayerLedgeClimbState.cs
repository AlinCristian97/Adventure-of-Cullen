using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;

    private bool _ledgeHasCeiling;

    private int InputX;
    private int InputY;

    public PlayerLedgeClimbState(Player player) : base(player)
    {
    }

    public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityZero();
        Player.transform.position = _detectedPosition;
        _cornerPosition = Player.DetermineCornerPosition();

        _startPosition.Set(_cornerPosition.x - (Player.FacingDirection * Player.Data.StartOffset.x),
            _cornerPosition.y - Player.Data.StartOffset.y);

        _stopPosition.Set(_cornerPosition.x + (Player.FacingDirection * Player.Data.StopOffset.x),
            _cornerPosition.y + Player.Data.StopOffset.y);

        Player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        IsHanging = false;

        if (IsClimbing)
        {
            Player.transform.position = _stopPosition;
            IsClimbing = false;
        }
    }

    public override void Execute()
    {
        base.Execute();

        if (IsAnimationFinished)
        {
            if (_ledgeHasCeiling)
            {
                StateMachine.ChangeState(Player.States.CrouchIdleState);
            }
            else
            {
                StateMachine.ChangeState(Player.States.IdleState);
            }
        }
        else
        {
            InputX = Player.InputHandler.NormalizedInputX;
            InputY = Player.InputHandler.NormalizedInputY;
            JumpInput = Player.InputHandler.JumpInput;

            Player.SetVelocityZero();
            Player.transform.position = _startPosition;

            if (InputX == Player.FacingDirection && IsHanging && !IsClimbing)
            {
                CheckCeilingLedge();
                IsClimbing = true;
                Player.Components.Animator.SetBool("climbLedge", true);
            }
            else if (InputY == -1 && IsHanging && !IsClimbing)
            {
                StateMachine.ChangeState(Player.States.AirState);
            }
            else if (JumpInput && !IsClimbing)
            {
                Player.States.WallJumpState.DetermineWallJumpDirection(true);
                StateMachine.ChangeState(Player.States.WallJumpState);
            }
        }
    }

    private void CheckCeilingLedge()
    {
        RaycastHit2D hit = Physics2D.Raycast(_cornerPosition + (Vector2.up * 0.015f)
                                                             + (Vector2.right * (Player.FacingDirection * 0.35f)),
            Vector2.up, Player.Data.StandColliderHeight, Player.Data.WhatIsGround);

        //Debug
        Debug.DrawRay(_cornerPosition + (Vector2.up * 0.015f)
                                      + (Vector2.right * (Player.FacingDirection * 0.35f)),
            Vector2.up * Player.Data.StandColliderHeight, Color.magenta);

        _ledgeHasCeiling = hit;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        IsHanging = true;
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        Player.Components.Animator.SetBool("climbLedge", false);
    }
}