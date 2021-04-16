using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 RawMovementInput { get; set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        // Get rid of Y component of RawMovementInput
        NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }
    
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        
    }
}