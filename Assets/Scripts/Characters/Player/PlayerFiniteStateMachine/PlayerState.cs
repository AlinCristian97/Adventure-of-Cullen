using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player Player;
    protected PlayerStateMachine StateMachine;
    protected PlayerData PlayerData;

    protected float StartTime;
    
    private string _animatorBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        PlayerData = playerData;
        _animatorBoolName = animatorBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        StartTime = Time.time;
        
        Player.Animator.SetBool(_animatorBoolName, true);
        
        //test
        Debug.Log(_animatorBoolName);
    }

    public virtual void Exit()
    {
        Player.Animator.SetBool(_animatorBoolName, false);
    }
    
    public virtual void LogicUpdate()
    {
        
    }
    
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
         
    }
}