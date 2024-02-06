using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateMachineAgent : MonoBehaviour
{
    private Transform thisTransform;
    public Transform GetTransform => thisTransform;
    [SerializeField] protected string entryState = "Fight";
    protected Dictionary<string, BaseState> States;
    protected BaseState currentState;
    public Action<StateMachineAgent> OnFinishWork;
    public string CurrentStateName => currentState?.StateName;
    [NonSerialized]
    public bool IsWinner = false;
    public virtual void TransitionToState(string stateName)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = States[stateName];
        currentState.EnterState(this);
    }
    public void DoWork()
    {
        currentState.DoWork(this);
    }
    public void CallFinishWork()
    {
        OnFinishWork?.Invoke(this);
    }

    protected virtual void Start()
    {
        thisTransform = transform;
        States = new Dictionary<string, BaseState>();
    }

    protected virtual void Update()
    {
        DoWork();
    }

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 10;
        Handles.color = Color.white;
        Handles.Label(transform.position + Vector3.up * 0.4f + Vector3.left * 0.4f, $"{CurrentStateName}", style);
    }
#endif
    protected virtual void OnDestroy()
    {
      
    }

}