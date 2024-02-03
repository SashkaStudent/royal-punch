using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField]
    float minDistance = 1f;
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
