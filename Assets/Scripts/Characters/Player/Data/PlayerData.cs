using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity;
    
    [Header("Jump State")]
    public float JumpVelocity;

    [Header("Air State")]
    public float CoyoteTime;
    public float VariableJumpHeightMultiplier;
    
    [Header("Wall Slide State")]
    public float WallSlideVelocity;
    
    [Header("Wall Climb State")]
    public float WallClimbVelocity;
    
    [Header("Wall Jump State")]
    public float WallJumpVelocity;
    public float WallJumpTime;
    public Vector2 WallJumpAngle;
    
    [Header("Ledge Climb State")]
    public Vector2 StartOffset;
    public Vector2 StopOffset;
    
    [Header("Crouch States")]
    public float CrouchMovementVelocity;

    public float CrouchColliderHeight;
    public float StandColliderHeight;
    
    [Header("Check Variables")]
    public float GroundCheckDistance;
    public LayerMask WhatIsGround;
    public float WallCheckDistance;
}