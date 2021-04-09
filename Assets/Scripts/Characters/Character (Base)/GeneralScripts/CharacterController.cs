using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private InputMaster _input;
    private Character _character;
    private Rigidbody2D _rigidbody;

    private float _directionX;

    private void Awake()
    {
        _input = new InputMaster();
        _character = GetComponent<Character>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Jump.performed += _ => _character.PerformJump();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        SetMovementDirectionX(GetMovementInput());
    }

    private void FixedUpdate()
    {
        ExecuteMovement(GetNewVelocity());
    }
    
    private void SetMovementDirectionX(float directionX)
    {
        _directionX = directionX;
    }

    private float GetMovementInput()
    {
        return _input.Player.Move.ReadValue<float>();
    }
    
    private void ExecuteMovement(Vector2 newVelocity)
    {
        _rigidbody.velocity = newVelocity;
    }

    private Vector2 GetNewVelocity()
    {
        Vector2 newVelocity = new Vector2(CalculateVelocityX(), _rigidbody.velocity.y);

        return newVelocity;
    }

    private float CalculateVelocityX()
    {
        float velocityX = _directionX * _character.Stats.MovementSpeed * Time.fixedDeltaTime;
        return velocityX;
    }
}