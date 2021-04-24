namespace Player.FSM.States
{
    public class PlayerJumpState : PlayerAbilityState
    {
        public PlayerJumpState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Player.InputHandler.UseJumpInput();
            Player.VelocityModifier.SetVelocityY(Player.StatesData._jumpVelocity);
            IsAbilityDone = true;
        }
    }
}