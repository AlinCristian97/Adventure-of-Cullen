using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    [field: SerializeField] public float MovementSpeed { get; private set; }
    public Vector2 Direction { get; set; }
}
