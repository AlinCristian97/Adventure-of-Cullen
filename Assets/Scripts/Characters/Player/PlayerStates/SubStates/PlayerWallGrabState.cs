using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 _holdPosition;
    
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _holdPosition = Player.transform.position;
        
        HoldPosition();
    }
    
    public override void Execute()
    {
        base.Execute();
        
        if (!IsExitingState)
        {
            HoldPosition();

            if (InputY > 0)
            {
                StateMachine.ChangeState(Player.WallClimbState);
            }
            else if (InputY < 0 || !GrabInput)
            {
                StateMachine.ChangeState(Player.WallSlideState);
            }
        }
    }

    private void HoldPosition()
    {
        Player.transform.position = _holdPosition;
        
        Player.SetVelocityX(0f); // For cinemachine to not bug
        Player.SetVelocityY(0f); // For cinemachine to not bug
    }
}