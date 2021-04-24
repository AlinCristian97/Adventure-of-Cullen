using UnityEngine;

namespace Player.ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "newPlayerStatesData", menuName = "Data/Player Data/States Data")]
    public class PlayerStatesData : ScriptableObject
    {
        [Header("Move State")]
        public float _movementVelocity;
    
        [Header("Jump State")]
        public float _jumpVelocity;

        // [Header("Air State")]
        // public float CoyoteTime;
        // public float VariableJumpHeightMultiplier;
    
        [Header("Wall Slide State")]
        public float _wallSlideVelocity;
    
        [Header("Wall Climb State")]
        public float _wallClimbVelocity;
    
        [Header("Wall Jump State")]
        public float _wallJumpVelocity;
        public float _wallJumpTime;
        public Vector2 _wallJumpAngle;
    
        [Header("Ledge Climb State")]
        public Vector2 _startOffset;
        public Vector2 _stopOffset;
    
        [Header("Crouch States")]
        public float _crouchMovementVelocity;

        public float _crouchColliderHeight;
        public float _standColliderHeight;
    }
}