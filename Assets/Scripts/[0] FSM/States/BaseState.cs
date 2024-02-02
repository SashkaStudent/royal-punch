using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public string StateName => stateName;
    protected string stateName;
    public abstract void EnterState(StateMachineAgent agent);
    public abstract void ExitState(StateMachineAgent agent);
    public abstract void DoWork(StateMachineAgent agent);
    protected void ExecuteAction(Action<Action> action, Action callback)
    {
        if (action != null)
            action(callback);
        else callback();
    }
    protected bool LookCollisionByDirection(string layer, Vector3 position, Vector3 direction)
    {
        Collider2D collision = Physics2D.OverlapBox(position + direction, Vector2.one * 0.5f, 0, LayerMask.GetMask(layer));
        return collision != null;
    }
    protected bool LookGroundInDown(Vector3 position)
    {
        return LookCollisionByDirection("Ground", position, Vector3.down);
    }
}