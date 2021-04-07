using StrategyPattern.Interfaces;
using UnityEngine;

namespace StrategyPattern.Behaviours
{
    public class NoJump : IJumpBehaviour
    {
        public void Jump(Rigidbody2D rigidbody, float jumpForce)
        {
            // no jump
        }
    }
}