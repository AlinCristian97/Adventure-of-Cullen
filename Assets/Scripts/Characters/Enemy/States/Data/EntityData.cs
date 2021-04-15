using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class EntityData : ScriptableObject
{
    public float WallCheckDistance;
    public float LedgeCheckDistance;

    public LayerMask WhatIsGround;
}