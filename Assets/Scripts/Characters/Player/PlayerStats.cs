using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float WallSlideSpeed { get; private set; }
}