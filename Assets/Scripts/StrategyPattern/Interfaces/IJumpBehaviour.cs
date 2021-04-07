using UnityEngine;

namespace StrategyPattern.Interfaces
{
    public interface IJumpBehaviour
    {
        void Jump(Rigidbody2D rigidbody, float jumpForce);
    }
}