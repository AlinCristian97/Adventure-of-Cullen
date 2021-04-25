using UnityEngine;

namespace Player.ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "newPlayerChecksData", menuName = "Data/Player Data/Checks Data")]
    public class PlayerChecksData : ScriptableObject
    {
        [Header("Ground Check")]
        public float _groundCheckDistance;
        public float _groundCheckWidth;
        public LayerMask _whatIsGround;
        
        [Header("Ceiling Check")]
        public float _ceilingCheckDistance;
        public float _ceilingCheckWidth;

        [Header("Wall Check")]
        public float _wallCheckDistance;
    }
}