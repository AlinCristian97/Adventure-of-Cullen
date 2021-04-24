using Common.FSM;
using Common.Modifiers;
using Player.ScriptableObjects.Data;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        public PlayerComponents Components { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        public StateMachine StateMachine { get; private set; }
        public PlayerStates States { get; private set; }
        public PlayerChecks Checks { get; private set; }

        #region Modifiers

        public VelocityModifier VelocityModifier { get; private set; }
        public ColliderModifier ColliderModifier { get; private set; }

        #endregion

        #region Data SO

        [field: SerializeField] public PlayerStatesData StatesData { get; private set; }
        [field: SerializeField] public PlayerChecksData ChecksData { get; private set; }

        #endregion
        
        public PlayerUtilities Utilities { get; private set; }
        
        #region Debug Variables

        private bool testGrabInput;
        private bool testJumpInput;
        private bool testIsGrounded;
        private bool testIsTouchingWall;
        private bool testIsTouchingLedge;
        private bool testIsTouchingCeiling;
    
        private string testCurrentState;

        #endregion
        
        #region Unity Callback Functions

        private void Awake()
        {
            Components = new PlayerComponents(
                GetComponent<Rigidbody2D>(),
                GetComponent<CapsuleCollider2D>(),
                GetComponent<Animator>());
            InputHandler = GetComponent<PlayerInputHandler>();
            StateMachine = new StateMachine();
            States = new PlayerStates(this);
            Checks = new PlayerChecks(this);
            
            VelocityModifier = new VelocityModifier(Components.Rigidbody);
            ColliderModifier = new ColliderModifier(Components.Collider);
            
            Utilities = new PlayerUtilities(this);
        }
        
        private void Start()
        {
            StateMachine.Initialize(States.IdleState);
        }

        private void Update()
        {
            StateMachine.CurrentState.Execute();
        
            // Debug
            testGrabInput = InputHandler.GrabInput;
            testJumpInput = InputHandler.JumpInput;
            testIsGrounded = Checks.CheckGrounded();
            testIsTouchingWall = Checks.CheckWall();
            testIsTouchingLedge = Checks.CheckLedge();
            testIsTouchingCeiling = Checks.CheckCeiling();
            testCurrentState = StateMachine.CurrentState.ToString();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.ExecutePhysics();
        }

        #endregion
    }
}