using UnityEngine;

namespace FiniteStateMachine
{
    public class State
    {
        protected FiniteStateMachine StateMachine;
        protected Entity Entity;

        protected float StartTime;

        protected string AnimatorBoolName;

        public State(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName)
        {
            Entity = entity;
            StateMachine = stateMachine;
            AnimatorBoolName = animatorBoolName;
        }

        public virtual void Enter()
        {
            StartTime = Time.time;
            Entity.Animator.SetBool(AnimatorBoolName, true);
        }

        public virtual void Exit()
        {
            Entity.Animator.SetBool(AnimatorBoolName, false);

        }

        public virtual void LogicUpdate()
        {
            
        }
        
        public virtual void PhysicsUpdate()
        {
            
        }
    }
}