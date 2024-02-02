using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public BaseInput input; //Zenject
    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent(out input)) return;

        input.OnMove += MoveHandler;
        input.OnEndMove += OnEndMove;
    }

    private void OnEndMove()
    {
    }

    private void MoveHandler(Vector2 direction)
    {
        Vector3 localDirection = transform.InverseTransformDirection(direction);
        transform.position += localDirection;
        transform.LookAt(Vector3.zero);
    }

    private void OnDestroy()
    {
        if (input == null) return;

        input.OnMove -= MoveHandler;
        input.OnEndMove -= OnEndMove;


    }
}
