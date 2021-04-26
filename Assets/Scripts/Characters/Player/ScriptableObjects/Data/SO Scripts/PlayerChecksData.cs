using UnityEngine;

namespace Player.ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "newPlayerChecksData", menuName = "Data/Player Data/Checks Data")]
    public class PlayerChecksData : ScriptableObject
    {
        public LayerMask _whatIsGround;

        [Header("Ground Check")]
        public float _groundCastHeight;
        public float _groundCastWidth;
        
        [Header("Ceiling Check")]
        public float _ceilingCastHeight;
        public float _ceilingCastWidth;

        [Header("Wall Check")]
        public float _wallCastHeight;
        public float _wallCastWidth;

        [Header("Ledge Check")]
        public float _ledgeCastOriginOffset;
    }
}