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

        public bool CheckGrounded() //TODO: Improve to start at the bottom of the collider
        {
            float horizontalSizeReductionFactor = 0.8f;
            Bounds bounds = _player.Components.Collider.bounds;
            Vector2 boxCastSize = new Vector2(bounds.size.x * horizontalSizeReductionFactor, bounds.size.y);
        
            RaycastHit2D hit = Physics2D.BoxCast(bounds.center, boxCastSize, 0,
                Vector2.down, _player.ChecksData._groundCheckDistance, _player.ChecksData._whatIsGround);
        
            // //Debug
            // Debug.DrawRay(
            //     bounds.center + new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0),
            //     Vector3.down * (bounds.extents.y + _player.ChecksData._groundCheckDistance),
            //     Color.blue);
            //
            // Debug.DrawRay(
            //     bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0), 
            //     Vector3.down * (bounds.extents.y + _player.ChecksData._groundCheckDistance),
            //     Color.blue);
            //
            // Debug.DrawRay(
            //     bounds.center - new Vector3(bounds.extents.x * horizontalSizeReductionFactor,
            //         bounds.extents.y + _player.ChecksData._groundCheckDistance),
            //     Vector3.right * (bounds.size.x * horizontalSizeReductionFactor),
            //     Color.blue);
        
            return hit;
        }
    
        public bool CheckCeiling()
        {
            //TODO: Improve code Separate debug into a separate function! Why is it /4 for debug?
            float horizontalSizeReductionFactor = 0.8f;
            Bounds bounds = _player.Components.Collider.bounds;
            Vector2 boxCastSize = new Vector2(bounds.size.x * horizontalSizeReductionFactor, bounds.size.y / 2);
        
            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(bounds.center.x, bounds.max.y), boxCastSize, 0,
                Vector2.up, _player.ChecksData._groundCheckDistance, _player.ChecksData._whatIsGround);
        
            // //Debug
            // Debug.DrawRay(
            //     new Vector3(bounds.center.x, bounds.max.y) + new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0),
            //     Vector2.up * (bounds.size.y / 4 + _playerData.GroundCheckDistance),
            //     Color.red);
            //
            // Debug.DrawRay(
            //     new Vector3(bounds.center.x, bounds.max.y) - new Vector3(bounds.extents.x * horizontalSizeReductionFactor, 0), 
            //     Vector2.up * (bounds.size.y / 4 + _playerData.GroundCheckDistance),
            //     Color.red);
            //
            // Debug.DrawRay(
            //     new Vector3(bounds.center.x, bounds.max.y) + new Vector3(bounds.extents.x * horizontalSizeReductionFactor,
            //         bounds.size.y / 4 + _playerData.GroundCheckDistance),
            //     Vector2.left * (bounds.size.x * horizontalSizeReductionFactor),
            //     Color.red);
        
            return hit;
        }

    

        //TODO: Check Touching Wall as BoxCast instead of Raycast?
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