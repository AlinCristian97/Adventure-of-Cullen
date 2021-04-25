using UnityEngine;

namespace Player
{
    public class PlayerChecks
    {
        private Player _player;

        public PlayerChecks(Player player)
        {
            _player = player;
        }
    
        public bool CheckFlip(int xInput)
        {
            bool shouldFlip = xInput != 0 && xInput != _player.Utilities.FacingDirection;

            return shouldFlip;
        }

        public bool CheckGrounded()
        {
            //TODO: add VerticalWidth to Scriptableobject

            const float verticalWidth = 0.8f;
            Bounds bounds = _player.Components.Collider.bounds;
            
            Vector3 castOrigin = new Vector2(bounds.center.x, bounds.min.y);
            Vector2 castSize = new Vector2(
                bounds.size.x * verticalWidth,
                _player.ChecksData._groundCheckDistance);
            const float castDistance = 0f;
        
            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, 0,
                Vector2.down, castDistance, _player.ChecksData._whatIsGround);

            // Debug
            DrawBoxCast();
            void DrawBoxCast()
            {
                float halfGroundCheckDistance = _player.ChecksData._groundCheckDistance / 2;
                float halfCastSizeY = castSize.y / 2f;
                float drawDistance = halfCastSizeY + castDistance;
                
                // Horizontals
                    // Bottom-right
                Debug.DrawRay(
                    castOrigin + new Vector3(bounds.extents.x * verticalWidth, 0),
                    Vector3.down * drawDistance,
                    Color.blue);
                
                    // Bottom-left
                Debug.DrawRay(
                    castOrigin - new Vector3(bounds.extents.x * verticalWidth, 0), 
                    Vector3.down * drawDistance,
                    Color.blue);
                
                    // Top-right
                Debug.DrawRay(
                    castOrigin + new Vector3(bounds.extents.x * verticalWidth, 0),
                    Vector3.up * halfGroundCheckDistance,
                    Color.blue);
            
                    // Top-left
                Debug.DrawRay(
                    castOrigin - new Vector3(bounds.extents.x * verticalWidth, 0), 
                    Vector3.up * halfGroundCheckDistance,
                    Color.blue);
            
                // Top and Bottom
                    // Bottom
                Debug.DrawRay(
                    castOrigin - new Vector3(bounds.extents.x * verticalWidth, drawDistance),
                    Vector3.right * (bounds.size.x * verticalWidth),
                    Color.blue);
                
                    // Top
                Debug.DrawRay(
                    castOrigin + new Vector3(bounds.extents.x * verticalWidth, halfGroundCheckDistance),
                    Vector3.left * (bounds.size.x * verticalWidth),
                    Color.blue);
            }
            
            return hit;
        }
    
        public bool CheckCeiling()
        {
            //TODO: add VerticalWidth to Scriptableobject
            const float verticalWidth = 0.8f;
            Bounds bounds = _player.Components.Collider.bounds;
            
            Vector3 castOrigin = new Vector2(bounds.center.x, bounds.max.y);
            Vector2 castSize = new Vector2(
                bounds.size.x * verticalWidth,
                _player.ChecksData._ceilingCheckDistance);
            const float castDistance = 0f;

            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, 0,
                Vector2.up, castDistance, _player.ChecksData._whatIsGround);
        
            // Debug
            DrawBoxCast();
            void DrawBoxCast()
            {
                float halfGroundCheckDistance = _player.ChecksData._groundCheckDistance / 2;
                float halfCastSizeY = castSize.y / 2f;
                float drawDistance = halfCastSizeY + castDistance;

                // Horizontals
                    // Bottom-right
                Debug.DrawRay(
                    castOrigin + new Vector3(bounds.extents.x * verticalWidth, 0),
                    Vector3.up * drawDistance,
                    Color.red);

                    // Bottom-left
                Debug.DrawRay(
                    castOrigin - new Vector3(bounds.extents.x * verticalWidth, 0),
                    Vector3.up * drawDistance,
                    Color.red);
                
                    // Top-right
                Debug.DrawRay(
                    castOrigin + new Vector3(bounds.extents.x * verticalWidth, 0),
                    Vector3.down * halfGroundCheckDistance,
                    Color.red);
            
                    // Top-left
                Debug.DrawRay(
                    castOrigin - new Vector3(bounds.extents.x * verticalWidth, 0), 
                    Vector3.down * halfGroundCheckDistance,
                    Color.red);

                            
                // Top and Bottom
                    // Bottom
                Debug.DrawRay(
                    castOrigin - new Vector3(bounds.extents.x * verticalWidth, drawDistance),
                    Vector2.right * (bounds.size.x * verticalWidth),
                    Color.red);
                
                    // Top
                    Debug.DrawRay(
                        castOrigin + new Vector3(bounds.extents.x * verticalWidth, drawDistance),
                        Vector2.left * (bounds.size.x * verticalWidth),
                        Color.red);
            }

            return hit;
        }

        //TODO: Check Touching Wall as BoxCast instead of Raycast? With smaller height
        public bool CheckWall()
        {
            //TODO: Improve code Separate debug into a separate function
            Bounds bounds = _player.Components.Collider.bounds;

            RaycastHit2D hit = Physics2D.Raycast(bounds.center,
                Vector2.right * _player.Utilities.FacingDirection,
                bounds.extents.x + _player.ChecksData._wallCheckDistance,
                _player.ChecksData._whatIsGround);
        
            //Debug
            // Debug.DrawRay(bounds.center,
            //     new Vector2((bounds.extents.x + _playerData.WallCheckDistance) * FacingDirection, 0),
            //     Color.yellow);
        
            return hit;
        }

        public bool CheckLedge()
        {
            //TODO: Improve code Separate debug into a separate function
            float offset = 1.5f;
            Bounds bounds = _player.Components.Collider.bounds;

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y / offset),
                Vector2.right * _player.Utilities.FacingDirection,
                bounds.extents.x + _player.ChecksData._wallCheckDistance,
                _player.ChecksData._whatIsGround);
        
            //Debug
            // Debug.DrawRay(new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y / offset),
            //     new Vector2((bounds.extents.x + _playerData.WallCheckDistance) * FacingDirection, 0),
            //     Color.red);
        
            return hit;
        }
    }
}