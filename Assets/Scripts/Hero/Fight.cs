using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField]
    private float minDistance = 1f;
    public float MinDistance => minDistance;

    private Animator animator;


    // Update is called once per frame

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if(transform.position.magnitude <= minDistance)
        {
            animator.SetLayerWeight(1, 1);
        } else
        {
            animator.SetLayerWeight(1, 0);

        }
    }
}
