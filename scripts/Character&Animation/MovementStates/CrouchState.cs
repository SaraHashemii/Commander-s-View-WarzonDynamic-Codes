using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : BaseState
{
    public override void EnterState(CharacterMovementManager characterMovementManager)

    {
        characterMovementManager.animator.SetBool("isCrouching", true);
    }

    public override void UpdateState(CharacterMovementManager characterMovementManager)

    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(characterMovementManager, characterMovementManager.runState);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (characterMovementManager.direction.magnitude < 0.1f)
            {
                ExitState(characterMovementManager, characterMovementManager.idleState);
            }
            else
            {
                ExitState(characterMovementManager, characterMovementManager.walkState);
            }
        }

        if (characterMovementManager.verticalInput < 0)
        {
            characterMovementManager.currentMoveSpeed = characterMovementManager.crouchBackSpeed;
        }
        else
        {
            characterMovementManager.currentMoveSpeed = characterMovementManager.crouchSpeed;
        }
    }

    public void ExitState(CharacterMovementManager characterMovementManager, BaseState state)
    {
        characterMovementManager.animator.SetBool("isCrouching", false);
        characterMovementManager.SwitchState(state);
    }

}
