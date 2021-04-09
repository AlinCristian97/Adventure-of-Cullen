using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterComponents
{
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
}
