using System;
using UnityEngine;

namespace FiniteStateMachine
{
    public class Entity : MonoBehaviour
    {
        public FiniteStateMachine StateMachine;

        public EntityData EntityData;
        public int FacingDirection { get; private set; }
        
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        private Vector2 _velocityWorkspace;
        [SerializeField] private Transform _wallCheck;
        [SerializeField] private Transform _ledgeCheck;

        public virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            StateMachine = new FiniteStateMachine();
        }

        public virtual void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }

        public virtual void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();;
        }

        public virtual void SetVelocity(float velocity)
        {
            _velocityWorkspace.Set(FacingDirection * velocity, Rigidbody.velocity.y);
            Rigidbody.velocity = _velocityWorkspace;
        }

        public virtual bool CheckWall()
        {
            return Physics2D.Raycast(_wallCheck.position, transform.right,
                EntityData.WallCheckDistance, EntityData.WhatIsGround);
        }

        public virtual bool CheckLedge()
        {
            return Physics2D.Raycast(_ledgeCheck.position, Vector2.down,
                EntityData.LedgeCheckDistance, EntityData.WhatIsGround);
        }

        public virtual void Flip()
        {
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}