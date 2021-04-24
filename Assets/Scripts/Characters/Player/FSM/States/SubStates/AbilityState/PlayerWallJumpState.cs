using UnityEngine;

namespace Player.FSM.States
{
    public class PlayerWallJumpState : PlayerAbilityState
    {
        private int _wallJumpDirection;
        private float _startTime;

        public PlayerWallJumpState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Player.InputHandler.UseJumpInput();
            Player.VelocityModifier.SetVelocityWallJump(Player.StatesData._wallJumpVelocity, Player.StatesData._wallJumpAngle, _wallJumpDirection);
            Player.Utilities.HandleFlip(_wallJumpDirection);
            _startTime = Time.time;
        }

        public override void Execute()
        {
            base.Execute();

            Player.Components.Animator.SetFloat("yVelocity", Player.Components.Rigidbody.velocity.y);

            if (Time.time >= _startTime + Player.StatesData._wallJumpTime)
            {
                IsAbilityDone = true;
            }
        }

        public void DetermineWallJumpDirection(bool isTouchingWall)
        {
            if (isTouchingWall)
            {
                _wallJumpDirection = -Player.Utilities.FacingDirection;
            }
            else
            {
                _wallJumpDirection = Player.Utilities.FacingDirection;
            }
        }
    }
}