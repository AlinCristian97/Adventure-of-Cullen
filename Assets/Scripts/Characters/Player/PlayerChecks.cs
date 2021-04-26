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
            Bounds bounds = _player.Components.Collider.bounds;
            
            Vector2 castOrigin = new Vector2(bounds.center.x, bounds.min.y);
            Vector2 castSize = new Vector2(
                _player.ChecksData._groundCastWidth,
                _player.ChecksData._groundCastHeight);
            const float castAngle = 0;
            Vector2 castDirection = Vector2.down;
            const float castDistance = 0f;
            LayerMask castLayerMask = _player.ChecksData._whatIsGround;
            
            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, castAngle,
                castDirection, castDistance, castLayerMask);

            #region Debug

            DrawBoxCast();
            void DrawBoxCast()
            {
                float halfGroundCheckWidth = _player.ChecksData._groundCastWidth / 2f;
                float halfGroundCheckDistance = _player.ChecksData._groundCastHeight / 2f;
                
                float halfCastSizeY = castSize.y / 2f;
                float drawDistance = halfCastSizeY + castDistance;
                
                Color castColor = Color.blue;

                DrawRightRay();
                DrawLeftRay();
                DrawTopRay();
                DrawBottomRay();
                
                void DrawRightRay()
                {
                    Vector2 rayOrigin = new Vector2(
                        castOrigin.x + halfGroundCheckWidth,
                        castOrigin.y + halfGroundCheckDistance);
                    Vector2 rayDirection = Vector2.down;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawLeftRay()
                {
                    Vector2 rayOrigin = new Vector2(
                        castOrigin.x - halfGroundCheckWidth,
                        castOrigin.y + halfGroundCheckDistance);
                    Vector2 rayDirection = Vector2.down;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawTopRay()
                {
                    Vector2 rayOrigin = castOrigin - new Vector2(halfGroundCheckWidth, drawDistance);
                    Vector2 rayDirection = Vector2.right;
                    float rayLength = castSize.x;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawBottomRay()
                {
                    Vector2 rayOrigin = castOrigin + new Vector2(halfGroundCheckWidth, halfGroundCheckDistance);
                    Vector2 rayDirection = Vector2.left;
                    float rayLength = castSize.x;

                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
            }   
            
            #endregion
            
            return hit;
        }
    
        public bool CheckCeiling()
        {
            Bounds bounds = _player.Components.Collider.bounds;
            
            Vector2 castOrigin = new Vector2(bounds.center.x, bounds.max.y);
            Vector2 castSize = new Vector2(
                _player.ChecksData._ceilingCastWidth,
                _player.ChecksData._ceilingCastHeight);
            const float castAngle = 0;
            Vector2 castDirection = Vector2.up;
            const float castDistance = 0f;
            LayerMask castLayerMask = _player.ChecksData._whatIsGround;

            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, castAngle,
                castDirection, castDistance, castLayerMask);

            #region Debug

            DrawBoxCast();
            void DrawBoxCast()
            {
                float halfCeilingCheckWidth = _player.ChecksData._ceilingCastWidth / 2f;
                float halfCeilingCheckDistance = _player.ChecksData._ceilingCastHeight / 2f;
                
                float halfCastSizeY = castSize.y / 2f;
                float drawDistance = halfCastSizeY + castDistance;
                
                Color castColor = Color.magenta;

                DrawRightRay();
                DrawLeftRay();
                DrawTopRay();
                DrawBottomRay();
                
                void DrawRightRay()
                {
                    Vector2 rayOrigin = new Vector2(
                        castOrigin.x + halfCeilingCheckWidth,
                        castOrigin.y + halfCeilingCheckDistance);
                    Vector2 rayDirection = Vector2.down;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawLeftRay()
                {
                    Vector2 rayOrigin = new Vector2(
                        castOrigin.x - halfCeilingCheckWidth,
                        castOrigin.y + halfCeilingCheckDistance);
                    Vector2 rayDirection = Vector2.down;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawTopRay()
                {
                    Vector2 rayOrigin = castOrigin - new Vector2(halfCeilingCheckWidth, drawDistance);
                    Vector2 rayDirection = Vector2.right;
                    float rayLength = castSize.x;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawBottomRay()
                {
                    Vector2 rayOrigin = castOrigin + new Vector2(halfCeilingCheckWidth, halfCeilingCheckDistance);
                    Vector2 rayDirection = Vector2.left;
                    float rayLength = castSize.x;

                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
            }
            
            #endregion
            
            return hit;
        }

        public bool CheckWall()
        {
            Bounds bounds = _player.Components.Collider.bounds;
            float facingBound = bounds.extents.x * _player.Utilities.FacingDirection;
            
            Vector2 castOrigin = new Vector2(bounds.center.x + facingBound, bounds.center.y);
            Vector2 castSize = new Vector2(
                _player.ChecksData._wallCastWidth,
                _player.ChecksData._wallCastHeight);
            const float castAngle = 0;
            Vector2 castDirection = Vector2.right;
            const float castDistance = 0f;
            LayerMask castLayerMask = _player.ChecksData._whatIsGround;
            
            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, castAngle,
                castDirection, castDistance, castLayerMask);
            
            #region Debug

            DrawBoxCast();
            void DrawBoxCast()
            {
                float halfWallCheckWidth = _player.ChecksData._wallCastWidth / 2f;
                float halfWallCheckDistance = _player.ChecksData._wallCastHeight / 2f;
                
                float halfCastSizeY = castSize.y / 2f;
                float drawDistance = halfCastSizeY + castDistance;
                
                Color castColor = Color.yellow;

                DrawRightRay();
                DrawLeftRay();
                DrawTopRay();
                DrawBottomRay();
                
                void DrawRightRay()
                {
                    Vector2 rayOrigin = new Vector2(
                        castOrigin.x + halfWallCheckWidth,
                        castOrigin.y + halfWallCheckDistance);
                    Vector2 rayDirection = Vector2.down;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawLeftRay()
                {
                    Vector2 rayOrigin = new Vector2(
                        castOrigin.x - halfWallCheckWidth,
                        castOrigin.y + halfWallCheckDistance);
                    Vector2 rayDirection = Vector2.down;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawTopRay()
                {
                    Vector2 rayOrigin = castOrigin - new Vector2(halfWallCheckWidth, drawDistance);
                    Vector2 rayDirection = Vector2.right;
                    float rayLength = castSize.x;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawBottomRay()
                {
                    Vector2 rayOrigin = castOrigin + new Vector2(halfWallCheckWidth, halfWallCheckDistance);
                    Vector2 rayDirection = Vector2.left;
                    float rayLength = castSize.x;

                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
            }   
            
            #endregion

            return hit;
        }

        public bool CheckLedge()
        {
            Bounds bounds = _player.Components.Collider.bounds;
            float facingBound = bounds.extents.x * _player.Utilities.FacingDirection;
            float originOffsetY = _player.ChecksData._ledgeCastOriginOffset;
            float halfWallCheckDistance = _player.ChecksData._wallCastWidth / 2f;

            Vector2 castOrigin = new Vector2(bounds.center.x + facingBound, bounds.center.y + originOffsetY);
            Vector2 castDirection = Vector2.right * _player.Utilities.FacingDirection;
            float castDistance = halfWallCheckDistance;
            LayerMask castLayerMask = _player.ChecksData._whatIsGround;

            RaycastHit2D hit = Physics2D.Raycast(castOrigin, castDirection, castDistance, castLayerMask);

            // Debug
            Debug.DrawRay(castOrigin, castDirection * castDistance, Color.red);
        
            return hit;
        }
    }
}