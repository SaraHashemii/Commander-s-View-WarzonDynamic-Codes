using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
    public override void EnterState(CharacterMovementManager characterMovementManager)

    {
        characterMovementManager.animator.SetBool("isRunning", true);
    }

    public override void UpdateState(CharacterMovementManager characterMovementManager)

    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(characterMovementManager, characterMovementManager.walkState);
        }
        else if (characterMovementManager.direction.magnitude < 0.1f )
        {
            ExitState(characterMovementManager, characterMovementManager.idleState);
        }

        if (characterMovementManager.verticalInput < 0)
        {
            characterMovementManager.currentMoveSpeed = characterMovementManager.runBackSpeed;
        }
        else
        {
            characterMovementManager.currentMoveSpeed = characterMovementManager.runSpeed;
        }
    }

    public void ExitState(CharacterMovementManager characterMovementManager, BaseState state)
    {
        characterMovementManager.animator.SetBool("isRunning", false);
        characterMovementManager.SwitchState(state);
    }


}

