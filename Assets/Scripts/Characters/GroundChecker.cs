using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;
    
    [SerializeField] private Character _character;

    private void FixedUpdate()
    {
        _character.IsGrounded = CheckGrounded();
    }

    private bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, _checkRadius);
    // }
}