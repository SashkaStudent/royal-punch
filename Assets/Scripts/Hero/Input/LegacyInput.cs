using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyInput : BaseInput
{
    void Update()
    {
        Vector3 direction = new (Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        CallMove(Vector3.ClampMagnitude(direction,1));
    }
}
