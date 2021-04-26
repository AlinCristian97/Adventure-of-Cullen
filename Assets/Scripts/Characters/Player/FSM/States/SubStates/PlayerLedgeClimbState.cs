using UnityEngine;

namespace Player.FSM.States
{
    public class PlayerLedgeClimbState : PlayerState
    {
        private Vector2 _detectedPosition;
        private Vector2 _cornerPosition;
        private Vector2 _startPosition;
        private Vector2 _stopPosition;

        private bool _ledgeHasCeiling;
        
        public PlayerLedgeClimbState(Player player) : base(player)
        {
        }

        public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;

        public override void Enter()
        {
            base.Enter();

            Player.VelocityModifier.SetVelocityZero();
            Player.transform.position = _detectedPosition;
            _cornerPosition = GetLedgeCornerPosition();

            _startPosition.Set(_cornerPosition.x - (Player.Utilities.FacingDirection * Player.StatesData._startOffset.x),
                _cornerPosition.y - Player.StatesData._startOffset.y);

            _stopPosition.Set(_cornerPosition.x + (Player.Utilities.FacingDirection * Player.StatesData._stopOffset.x),
                _cornerPosition.y + Player.StatesData._stopOffset.y);

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

                Player.VelocityModifier.SetVelocityZero();
                Player.transform.position = _startPosition;

                if (InputX == Player.Utilities.FacingDirection && IsHanging && !IsClimbing)
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

        private void CheckCeilingLedge() // TODO: Move in checks?
        {
            RaycastHit2D hit = Physics2D.Raycast(_cornerPosition + (Vector2.up * 0.015f)
                                                                 + (Vector2.right * (Player.Utilities.FacingDirection * 0.35f)),
                Vector2.up, Player.StatesData._standColliderHeight, Player.ChecksData._whatIsGround);

            //Debug
            Debug.DrawRay(_cornerPosition + (Vector2.up * 0.015f)
                                          + (Vector2.right * (Player.Utilities.FacingDirection * 0.35f)),
                Vector2.up * Player.StatesData._standColliderHeight, Color.magenta);

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

        private Vector2 GetLedgeCornerPosition()
        {
            //TODO: Improve code readability. Fix "Magic" Numbers
            float offset = 1.5f;
            Bounds bounds = Player.Components.Collider.bounds;

            RaycastHit2D xHit = Physics2D.Raycast(bounds.center, Vector2.right * Player.Utilities.FacingDirection,
                Player.ChecksData._wallCastHeight, Player.ChecksData._whatIsGround);
            float xDistance = xHit.distance;
        
            Vector2 cornerPosition = new Vector2((xDistance + 0.015f) * Player.Utilities.FacingDirection, 0f);
        
            RaycastHit2D yHit =
                Physics2D.Raycast(new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y / offset) + cornerPosition, Vector2.down,
                    bounds.center.y - (bounds.center.y + bounds.extents.y / 2) + 0.015f, Player.ChecksData._whatIsGround);
            float yDistance = yHit.distance;

            cornerPosition.Set(bounds.center.x + (xDistance * Player.Utilities.FacingDirection), (bounds.center.y + bounds.extents.y / offset) - yDistance);

            return cornerPosition;
        }
    }
}