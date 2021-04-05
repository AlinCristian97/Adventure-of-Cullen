using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected string _name;
    [field: SerializeField] public float MovementSpeed { get; private set; }
}