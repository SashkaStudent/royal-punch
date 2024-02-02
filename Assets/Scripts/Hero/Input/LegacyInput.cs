using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyInput : BaseInput
{
    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        CallMove(direction);
    }
}
