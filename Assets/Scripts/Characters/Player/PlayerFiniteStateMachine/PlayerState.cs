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
    protected bool IsExitingState; //TODO: Find another solution?

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

        IsExitingState = false;
        
        //test
        Debug.Log(this);
    }

    public virtual void Exit()
    {
        Player.Animator.SetBool(_animatorBoolName, false);
        
        IsExitingState = true;
    }
    
    public virtual void LogicUpdate()
    {
        
    }
    
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
    
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishedTrigger()
    {
    }
}