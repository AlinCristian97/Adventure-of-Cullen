using UnityEngine;

// public class PlayerLandState : PlayerGroundedState
// {
//     public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
//     {
//     }
//
//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//
//         if (InputX != 0)
//         {
//             StateMachine.ChangeState(Player.MoveState);
//         }
//         // else if (InputX == 0)
//         // {
//         //     StateMachine.ChangeState(Player.IdleState);
//         // }
//     }
// }