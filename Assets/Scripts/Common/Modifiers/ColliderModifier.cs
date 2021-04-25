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
            Vector2 currentOffset = _collider.offset;
            Vector2 currentSize = _collider.size;
        
            Vector2 newOffset = new Vector2(currentOffset.x, currentOffset.y + (height - currentSize.y) / 2);
            Vector2 newSize = new Vector2(currentSize.x, height);
            
            _collider.offset = newOffset;
            _collider.size = newSize;
        }
    }
}