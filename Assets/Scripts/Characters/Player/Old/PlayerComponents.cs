using UnityEngine;

[System.Serializable]
public class PlayerComponents
{
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public Collider2D Collider { get; private set; }
}