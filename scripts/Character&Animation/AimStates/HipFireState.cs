using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : BaseAimState
{
    public override void EnterState(CharacterAimManager aim)
    {
        aim.animator.SetBool("isAiming", false);
        aim.currentFov = aim.hipFireFov;
    }

    public override void UpdateState(CharacterAimManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.m_aimState);
        }
    }
}
