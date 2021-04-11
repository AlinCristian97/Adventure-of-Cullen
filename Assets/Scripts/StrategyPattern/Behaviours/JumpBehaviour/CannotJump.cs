using StrategyPattern.Interfaces;
using UnityEngine;

namespace StrategyPattern.Behaviours
{
    public class CannotJump : IJumpBehaviour
    {
        public void Jump(Rigidbody2D rigidbody, float jumpForce)
        {
            // can't jump
        }
    }
}