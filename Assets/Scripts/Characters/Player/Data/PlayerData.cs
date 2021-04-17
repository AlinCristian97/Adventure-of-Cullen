using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity;
    
    [Header("Jump State")]
    public float JumpVelocity;
    public int AmountOfJumps;
    
    [Header("Air State")]
    public float CoyoteTime;
    public float VariableJumpHeightMultiplier;
    
    [Header("Wall Slide State")]
    public float WallSlideVelocity;
    
    [Header("Wall Climb State")]
    public float WallClimbVelocity;
    
    [Header("Check Variables")]
    public float GroundCheckDistance;
    public LayerMask WhatIsGround;
    public float WallCheckDistance;
}