using Common.FSM;
using UnityEngine;

namespace Player.FSM
{
    public abstract class PlayerState : State
    {
        protected readonly Player Player;
    
        // Input
        protected int InputX;
        protected int InputY;
        protected bool GrabInput;
        protected bool JumpInput;

        // Surroundings
        protected bool IsGrounded;
        protected bool IsTouchingWall;
        protected bool IsTouchingLedge;
        protected bool IsTouchingCeiling;

        // State
        protected bool IsHanging;
        protected bool IsClimbing;

        // Keep?
        protected bool IsAnimationFinished;
        private string _animatorBoolName;
        protected bool IsExitingState; //TODO: Find another solution?

        // Initialize them in ctor?
        public PlayerState(Player player)
        {
            Player = player;
            StateMachine = Player.StateMachine;
        }

        public override void Enter()
        {
            CheckInput();
            CheckSurroundings();
        
            Player.Components.Animator.SetBool(_animatorBoolName, true);
        
            IsAnimationFinished = false;
            IsExitingState = false;
        
            // Debug
            Debug.Log(this);
        }

        public override void Exit()
        {
            Player.Components.Animator.SetBool(_animatorBoolName, false);
        
            IsExitingState = true;
        }

        public override void Execute()
        {
            CheckInput();
        }

        public override void ExecutePhysics()
        {
            CheckSurroundings();
        }

        private void CheckInput()
        {
            InputX = Player.InputHandler.NormalizedInputX;
            InputY = Player.InputHandler.NormalizedInputY;
            GrabInput = Player.InputHandler.GrabInput;
            JumpInput = Player.InputHandler.JumpInput;
        }

        private void CheckSurroundings()
        {
            IsGrounded = Player.Checks.CheckGrounded();
            IsTouchingWall = Player.Checks.CheckWall();
            IsTouchingLedge = Player.Checks.CheckLedge();
            IsTouchingCeiling = Player.Checks.CheckCeiling();
        }

        //Keep them?
        public virtual void AnimationTrigger() { }

        public virtual void AnimationFinishedTrigger() => IsAnimationFinished = true;
    }
}