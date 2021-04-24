namespace Player.FSM.States
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player player) : base(player)
        {
        }

        public override void Execute()
        {
            base.Execute();
        
            //TODO: Too many if/else statements? using COMMAND pattern? / Transitions?? FROM state TO state on CONDITION

            if (IsGrounded && Player.Components.Rigidbody.velocity.y < 0.01f)
            {
                StateMachine.ChangeState(Player.States.IdleState);
            }
            else if (IsTouchingWall && !IsTouchingLedge && !IsGrounded)
            {
                StateMachine.ChangeState(Player.States.LedgeClimbState);
            }
            else if (JumpInput && IsTouchingWall)
            {
                IsTouchingWall = Player.Checks.CheckWall();
                Player.States.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
                StateMachine.ChangeState(Player.States.WallJumpState);
            }
            else if (JumpInput && IsGrounded)
            {
                StateMachine.ChangeState(Player.States.JumpState);
            }
            else if (IsTouchingWall && GrabInput && IsTouchingLedge)
            {
                StateMachine.ChangeState(Player.States.WallGrabState);
            }
            else if (IsTouchingWall && InputX == Player.Utilities.FacingDirection && Player.Components.Rigidbody.velocity.y <= 0f)
            {
                StateMachine.ChangeState(Player.States.WallSlideState);
            }
            else
            {
                Player.Utilities.HandleFlip(InputX);
                Player.VelocityModifier.SetVelocityX(Player.StatesData._movementVelocity * InputX);

                Player.Components.Animator.SetFloat("yVelocity", Player.Components.Rigidbody.velocity.y);
            }



            if (IsTouchingWall && !IsTouchingLedge)
            {
                Player.States.LedgeClimbState.SetDetectedPosition(Player.transform.position);
            }
        }

        //TODO: Coyote Time
        //TODO: Jump Variable
    }
}