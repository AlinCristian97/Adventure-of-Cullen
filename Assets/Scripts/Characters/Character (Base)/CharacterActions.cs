using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions
{
    private Character _character;

    public CharacterActions(Character character)
    {
        _character = character;
    }

    public void Move(Transform transform)
    {
        _character.Components.Rigidbody.velocity = new Vector2(
            _character.Stats.Direction.x * _character.Stats.MovementSpeed * Time.fixedDeltaTime,
            _character.Stats.Direction.y);
    }
}
