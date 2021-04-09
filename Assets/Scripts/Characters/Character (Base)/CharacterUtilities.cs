using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUtilities
{
    private Character _character;
    private InputMaster _input;

    public CharacterUtilities(Character character)
    {
        _character = character;
        _input = new InputMaster();
        _input.Enable();
    }

    public void HandleInput()
    {
        _character.Stats.Direction = new Vector2(
            GetHorizontalMovementInput(),
            _character.Components.Rigidbody.velocity.y);
    }

    private float GetHorizontalMovementInput()
    {
        return _input.Player.Move.ReadValue<float>();
    }
}
