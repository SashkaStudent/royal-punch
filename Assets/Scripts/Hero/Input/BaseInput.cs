using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput : MonoBehaviour
{
    public event Action OnBeginMove;
    public event Action<Vector3> OnMove;
    public event Action OnEndMove;
    // Start is called before the first frame update
    protected virtual void CallBeginMove() => OnBeginMove?.Invoke();
    protected virtual void CallEndMove() => OnEndMove?.Invoke();
    protected virtual void CallMove(Vector3 direction) => OnMove?.Invoke(direction);

}
