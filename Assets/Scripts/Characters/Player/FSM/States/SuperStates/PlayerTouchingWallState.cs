namespace Player.FSM.States
{
    public abstract class PlayerTouchingWallState : PlayerState
    {
        protected PlayerTouchingWallState(Player player) : base(player)
        {
        }

        public override void Execute()
        {
            base.Execute();

            if (JumpInput)
            {
                Player.States.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
                StateMachine.ChangeState(Player.States.WallJumpState);
            }
            else if (IsGrounded && !GrabInput)
            {
                StateMachine.ChangeState(Player.States.IdleState);
            }
            else if (!IsTouchingWall || (InputX != Player.Utilities.FacingDirection && !GrabInput))
            {
                StateMachine.ChangeState(Player.States.AirState);
            }
            else if (IsTouchingWall && !IsTouchingLedge)
            {
                StateMachine.ChangeState(Player.States.LedgeClimbState);
            }
        
        
        
            if (IsTouchingWall && !IsTouchingLedge)
            {
                Player.States.LedgeClimbState.SetDetectedPosition(Player.transform.position);
            }
        }
    }
}