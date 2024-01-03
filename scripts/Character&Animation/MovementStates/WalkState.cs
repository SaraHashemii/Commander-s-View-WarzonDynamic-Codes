using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState
{
    public override void EnterState(CharacterMovementManager characterMovementManager)

    {
        characterMovementManager.animator.SetBool("isWalking", true);
    }

    public override void UpdateState(CharacterMovementManager characterMovementManager)

    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(characterMovementManager, characterMovementManager.runState);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ExitState(characterMovementManager, characterMovementManager.crouchState);
        }
        else if (characterMovementManager.direction.magnitude < 0.1f)
        {
            ExitState(characterMovementManager, characterMovementManager.idleState);
        }

        if (characterMovementManager.verticalInput < 0)
        {
            characterMovementManager.currentMoveSpeed = characterMovementManager.walkBackSpeed;
        }
        else
        {
            characterMovementManager.currentMoveSpeed = characterMovementManager.walkSpeed;
        }
    }

    public void ExitState(CharacterMovementManager characterMovementManager, BaseState state)
    {
        characterMovementManager.animator.SetBool("isWalking", false);
        characterMovementManager.SwitchState(state);
    }


}
