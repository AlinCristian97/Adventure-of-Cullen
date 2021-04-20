using UnityEngine;

public class PlayerComponents
{
    public Animator Animator { get; }
    public Rigidbody2D Rigidbody { get; }
    public CapsuleCollider2D Collider { get; }

    public PlayerComponents(Rigidbody2D rigidbody, CapsuleCollider2D collider, Animator animator)
    {
        Rigidbody = rigidbody;
        Collider = collider;
        Animator = animator;
    }
}