using StrategyPattern.Interfaces;
using UnityEngine;

namespace StrategyPattern.Behaviours
{
    public class NormalJump : IJumpBehaviour
    {
        public void Jump(Rigidbody2D rigidbody, float jumpForce)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}