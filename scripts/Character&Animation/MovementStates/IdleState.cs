using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public override void EnterState(CharacterMovementManager characterMovementManager)

    {

    }

    public override void UpdateState(CharacterMovementManager characterMovementManager)

    {
        if (characterMovementManager.direction.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                characterMovementManager.SwitchState(characterMovementManager.runState);
            }
            else
            {
                characterMovementManager.SwitchState(characterMovementManager.walkState);
            }

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            characterMovementManager.SwitchState(characterMovementManager.crouchState);
        }
    }


}
