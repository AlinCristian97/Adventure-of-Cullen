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
            
            Vector3 castOrigin = new Vector2(bounds.center.x, bounds.min.y);
            Vector2 castSize = new Vector2(
                _player.ChecksData._groundCheckWidth,
                _player.ChecksData._groundCheckDistance);
            const float castDistance = 0f;
            Color castColor = Color.blue;
        
            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, 0,
                Vector2.down, castDistance, _player.ChecksData._whatIsGround);

            // Debug
            DrawBoxCast();
            void DrawBoxCast()
            {
                float halfGroundCheckWidth = _player.ChecksData._groundCheckWidth / 2f;
                float halfGroundCheckDistance = _player.ChecksData._groundCheckDistance / 2f;
                
                float halfCastSizeY = castSize.y / 2f;
                float drawDistance = halfCastSizeY + castDistance;

                DrawRightRay();
                DrawLeftRay();
                DrawTopRay();
                DrawBottomRay();
                
                void DrawRightRay()
                {
                    Vector3 rayOrigin = new Vector3(
                        castOrigin.x + halfGroundCheckWidth,
                        castOrigin.y + halfGroundCheckDistance);
                    Vector3 rayDirection = Vector3.down;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                void DrawLeftRay()
                {
                    Vector3 rayOrigin = new Vector3(
                        castOrigin.x - halfGroundCheckWidth,
                        castOrigin.y + halfGroundCheckDistance);
                    Vector3 rayDirection = Vector3.down;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                void DrawTopRay()
                {
                    Vector3 rayOrigin = castOrigin - new Vector3(halfGroundCheckWidth, drawDistance);
                    Vector3 rayDirection = Vector3.right;
                    float rayLength = _player.ChecksData._groundCheckWidth;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                void DrawBottomRay()
                {
                    Vector3 rayOrigin = castOrigin + new Vector3(halfGroundCheckWidth, halfGroundCheckDistance);
                    Vector3 rayDirection = Vector3.left;
                    float rayLength = _player.ChecksData._groundCheckWidth;

                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
            }   
            
            return hit;
        }
    
        public bool CheckCeiling()
        {
            Bounds bounds = _player.Components.Collider.bounds;
            
            Vector3 castOrigin = new Vector2(bounds.center.x, bounds.max.y);
            Vector2 castSize = new Vector2(
                _player.ChecksData._ceilingCheckWidth,
                _player.ChecksData._ceilingCheckDistance);
            const float castDistance = 0f;
            Color castColor = Color.magenta;

            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, castSize, 0,
                Vector2.up, castDistance, _player.ChecksData._whatIsGround);
        
            // Debug
            DrawBoxCast();
            void DrawBoxCast()
            {
                float halfCeilingCheckWidth = _player.ChecksData._ceilingCheckWidth / 2f;
                float halfCeilingCheckDistance = _player.ChecksData._ceilingCheckDistance / 2f;
                
                float halfCastSizeY = castSize.y / 2f;
                float drawDistance = halfCastSizeY + castDistance;

                DrawRightRay();
                DrawLeftRay();
                DrawTopRay();
                DrawBottomRay();
                
                void DrawRightRay()
                {
                    Vector3 rayOrigin = new Vector3(
                        castOrigin.x + halfCeilingCheckWidth,
                        castOrigin.y + halfCeilingCheckDistance);
                    Vector3 rayDirection = Vector3.up;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawLeftRay()
                {
                    Vector3 rayOrigin = new Vector3(
                        castOrigin.x - halfCeilingCheckWidth,
                        castOrigin.y + halfCeilingCheckDistance);
                    Vector3 rayDirection = Vector3.up;
                    float rayLength = castSize.y + castDistance;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawTopRay()
                {
                    Vector3 rayOrigin = castOrigin - new Vector3(halfCeilingCheckWidth, drawDistance);
                    Vector3 rayDirection = Vector3.right;
                    float rayLength = _player.ChecksData._groundCheckWidth;
                    
                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
                
                void DrawBottomRay()
                {
                    Vector3 rayOrigin = castOrigin + new Vector3(halfCeilingCheckWidth, halfCeilingCheckDistance);
                    Vector3 rayDirection = Vector3.left;
                    float rayLength = _player.ChecksData._groundCheckWidth;

                    Debug.DrawRay(rayOrigin, rayDirection * rayLength, castColor);
                }
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