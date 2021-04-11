using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterComponents
{
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
}