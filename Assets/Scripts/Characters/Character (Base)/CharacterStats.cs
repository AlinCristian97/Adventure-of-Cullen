using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    public Vector2 Direction { get; set; }
}