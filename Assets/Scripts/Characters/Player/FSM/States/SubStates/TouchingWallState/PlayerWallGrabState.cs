using UnityEngine;

namespace Player.FSM.States
{
    public class PlayerWallGrabState : PlayerTouchingWallState
    {
        private Vector2 _holdPosition;

        public PlayerWallGrabState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _holdPosition = Player.transform.position;

            HoldPosition();
        }

        public override void Execute()
        {
            base.Execute();

            if (!IsExitingState)
            {
                HoldPosition();

                if (InputY > 0)
                {
                    StateMachine.ChangeState(Player.States.WallClimbState);
                }
                else if (InputY < 0 || !GrabInput)
                {
                    StateMachine.ChangeState(Player.States.WallSlideState);
                }
            }
        }

        private void HoldPosition()
        {
            Player.transform.position = _holdPosition;

            Player.VelocityModifier.SetVelocityX(0f); // For cinemachine to not bug
            Player.VelocityModifier.SetVelocityY(0f); // For cinemachine to not bug
        }
    }
}