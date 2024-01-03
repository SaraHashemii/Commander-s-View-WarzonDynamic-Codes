using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : BaseAimState
{
    public override void EnterState(CharacterAimManager aim)
    {
        aim.animator.SetBool("isAiming", true);
        aim.currentFov = aim.aimingFov;
    }

    public override void UpdateState(CharacterAimManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.m_hipFireState);
        }
    }
}
