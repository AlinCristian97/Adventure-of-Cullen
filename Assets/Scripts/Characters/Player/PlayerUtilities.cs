namespace Player
{
    public class PlayerUtilities
    {
        public int FacingDirection { get; private set; } = 1;

        private readonly Player _player;

        public PlayerUtilities(Player player)
        {
            _player = player;
        }

        public void HandleFlip(int xInput)
        {
            if (_player.Checks.CheckFlip(xInput))
            {
                FacingDirection *= -1;
                _player.transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
    }
}