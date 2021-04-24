using UnityEngine;

namespace Common.Modifiers
{
    public class ColliderModifier
    {
        private readonly CapsuleCollider2D _collider;

        public ColliderModifier(CapsuleCollider2D collider)
        {
            _collider = collider;
        }

        public void SetColliderHeight(float height)
        {
            //TODO: Improve code readability

            Vector2 center = _collider.offset;
            Vector2 currentSize = _collider.size;
        
            Vector2 newSize = new Vector2(currentSize.x, height);

            center.y += (height - currentSize.y) / 2;
        
            currentSize = newSize;
            _collider.size = currentSize;
            _collider.offset = center;
        }
    }
}