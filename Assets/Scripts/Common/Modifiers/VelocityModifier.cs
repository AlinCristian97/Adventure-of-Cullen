using UnityEngine;

namespace Common.Modifiers
{
    public class VelocityModifier
    {
        private readonly Rigidbody2D _rigidbody;

        public VelocityModifier(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }
        
        //TODO: Improve overall readability
        
        public void SetVelocityZero()
        {
            _rigidbody.velocity = Vector2.zero;
        }
    
        public void SetVelocityWallJump(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            Vector2 newVelocity = new Vector2(angle.x * velocity * direction, angle.y * velocity);
        
            _rigidbody.velocity = newVelocity;
        }
    
        public void SetVelocityX(float velocity)
        {
            Vector2 newVelocity = new Vector2(velocity, _rigidbody.velocity.y);
        
            _rigidbody.velocity = newVelocity;
        }

        public void SetVelocityY(float velocity)
        {
            Vector2 newVelocity = new Vector2(_rigidbody.velocity.x, velocity);
        
            _rigidbody.velocity = newVelocity;
        }
    }
}