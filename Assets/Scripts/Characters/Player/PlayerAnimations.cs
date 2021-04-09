using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    private Character _character;
    private Animator _animator;

    private void Awake()
    {
        _character = GetComponentInParent<Character>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("yVelocity", _character.Components.Rigidbody.velocity.y);
    }

    public void TriggerJumpAnimation()
    {
        _animator.SetTrigger("jumped");
    }
}
