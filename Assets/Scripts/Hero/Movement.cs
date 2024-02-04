using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private BaseInput input; //Zenject
    private Animator animator;
    [SerializeField]
    private float minDistance = 0.5f;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float angleSpeed;

    [SerializeField]
    private GameObject mesh;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if (!TryGetComponent(out input) || animator == null) return;

        //input.OnMove += MoveHandler;
        //input.OnEndMove += OnEndMove;
    }

    void Start()
    {


    }

    private void OnEnable()
    {
        input.OnMove += MoveHandler;
        input.OnEndMove += OnEndMove;
    }

    private void OnDisable()
    {

        input.OnMove -= MoveHandler;
        input.OnEndMove -= OnEndMove;
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
    }
    private void OnEndMove()
    {
    }

    private void MoveHandler(Vector3 localDirection)
    {
        transform.LookAt(Vector3.zero);

        transform.RotateAround(Vector3.zero, Vector3.up, -localDirection.x * (angleSpeed / transform.position.magnitude) * Time.deltaTime);

        if(transform.position.magnitude > minDistance || localDirection.z < 0)
            transform.position += localDirection.z * speed * Time.deltaTime * transform.forward;

        Vector3 direction = transform.TransformDirection(new(localDirection.x, localDirection.y, Mathf.Abs(localDirection.z)));
        
        Vector3 meshLook = direction.sqrMagnitude > 0 ? direction : transform.forward; 
        Quaternion look = Quaternion.LookRotation(meshLook);
        mesh.transform.DORotateQuaternion(look, 0.2f);
        

        animator.SetFloat("Horizontal", localDirection.x);
        animator.SetFloat("Vertical", localDirection.z);
    }

    private void OnDestroy()
    {
        if (input == null) return;

        input.OnMove -= MoveHandler;
        input.OnEndMove -= OnEndMove;


    }
}
