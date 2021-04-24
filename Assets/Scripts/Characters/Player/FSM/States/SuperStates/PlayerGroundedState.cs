namespace Player.FSM.States
{
    public abstract class PlayerGroundedState : PlayerState
    {
        protected PlayerGroundedState(Player player) : base(player)
        {
        }

        public override void Execute()
        {
            base.Execute();

            if (JumpInput && IsGrounded)
            {
                StateMachine.ChangeState(Player.States.JumpState);
            }
            // else if (!IsGrounded)
            // {
            //     // Debug.Log("!isGrounded");
            //     // Player.AirState.StartCoyoteTime();
            //     // StateMachine.ChangeState(Player.AirState);
            // }
            else if (IsTouchingWall && GrabInput && IsTouchingLedge)
            {
                StateMachine.ChangeState(Player.States.WallGrabState);
            }
        }
    }
}