using UnityEngine;

[System.Serializable]
public class PlayerStats : CharacterStats
{
    [field: SerializeField] public float JumpForce { get; private set; }
}