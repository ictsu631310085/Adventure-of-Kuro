using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.Idle;
    }

    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        // Get Stunned
        if (playerData.stunTime > 0)
        {
            playerData.stunTime -= Time.deltaTime;
            playerData.currentSpeed = 0;
        }
        // Not Get Stunned
        else
        {
            playerData.currentSpeed = playerData.speed;

            // Walk
            if (Input.GetAxis("Horizontal") != 0)
            {
                stateManager.SwitchState(stateManager.WalkingState);
            }
            else
                // Jump
            if (Time.timeScale == 1 && playerData.isGrounded && Input.GetButtonDown("Jump"))
            {
                stateManager.SwitchState(stateManager.JumpingState);
            }
            else
                // Melee Attack
            if (Time.timeScale == 1 && Input.GetButtonDown("Attack"))
            {
                stateManager.SwitchState(stateManager.Melee1State);
            }

            // Shift Element
            // Right
            if (Time.timeScale == 1 && Input.GetButtonDown("Shift Element Right"))
            {
                playerData.ShiftElementRight();
            }
            // Left
            else if (Time.timeScale == 1 && Input.GetButtonDown("Shift Element Left"))
            {
                playerData.ShiftElementLeft();
            }

            // Dash
            if (Time.timeScale == 1 && playerData.currentMP > 0 && Input.GetButtonDown("Ability1"))
            {
                playerData.currentMP--;
                stateManager.SwitchState(stateManager.DashingState);
            }

            // Fall
            if (!playerData.isGrounded)
            {
                stateManager.SwitchState(stateManager.FallingState);
            }
        }
    }
}
