using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private Vector2 RawMovementInput { get; set; }
        public int NormalizedInputX { get; private set; }
        public int NormalizedInputY { get; private set; }
        public bool JumpInput { get; private set; }
        public bool JumpInputStop { get; private set; }
        public bool GrabInput { get; private set; }
        [SerializeField] private float _inputHoldTime;

        private float _jumpInputStartTime;

        private void Update()
        {
            CheckJumpInputHoldTime();
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();

            NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
    
        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                JumpInput = true;
                JumpInputStop = false;
                _jumpInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                JumpInputStop = true;
            }
        }

        public void OnGrabInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                GrabInput = true;
            }

            if (context.canceled)
            {
                GrabInput = false;
            }
        }

        public void UseJumpInput() => JumpInput = false;

        private void CheckJumpInputHoldTime()
        {
            if (Time.time >= _jumpInputStartTime + _inputHoldTime)
            {
                JumpInput = false;
            }
        }
    }
}