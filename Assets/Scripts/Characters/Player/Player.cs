using UnityEngine;

public class Player : Character
{
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _jumpBehaviour = new HumanJump();
    }
}