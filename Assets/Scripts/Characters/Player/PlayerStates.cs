using Player.FSM.States;

namespace Player
{
    public class PlayerStates
    {
        public PlayerIdleState IdleState { get; }
        public PlayerMoveState MoveState { get; }
        public PlayerJumpState JumpState { get; }
        public PlayerAirState AirState { get; }
        public PlayerWallSlideState WallSlideState { get; }
        public PlayerWallGrabState WallGrabState { get; }
        public PlayerWallClimbState WallClimbState { get; }
        public PlayerWallJumpState WallJumpState { get; }
        public PlayerCrouchIdleState CrouchIdleState { get; }
        public PlayerCrouchMoveState CrouchMoveState { get; }
        public PlayerLedgeClimbState LedgeClimbState { get; }

        public PlayerStates(Player player)
        {
            IdleState = new PlayerIdleState(player);
            MoveState = new PlayerMoveState(player);
            JumpState = new PlayerJumpState(player);
            AirState = new PlayerAirState(player);
            WallSlideState = new PlayerWallSlideState(player);
            WallGrabState = new PlayerWallGrabState(player);
            WallClimbState = new PlayerWallClimbState(player);
            WallJumpState = new PlayerWallJumpState(player);
            CrouchIdleState = new PlayerCrouchIdleState(player);
            CrouchMoveState = new PlayerCrouchMoveState(player);
            LedgeClimbState = new PlayerLedgeClimbState(player);
        }
    }
}