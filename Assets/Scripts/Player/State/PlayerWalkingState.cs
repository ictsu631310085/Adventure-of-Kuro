using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.Walking;
    }

    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        // Get Stunned
        if (playerData.stunTime > 0)
        {
            playerData.animator.SetBool("isWalking", false);
            stateManager.SwitchState(stateManager.IdleState);
        }
        // Not Get Stunned
        else
        {
            // Flip Sprite When Turn
            playerData.TurnCharacter();

            // Continue/Stop Walking
            playerData.moveX = Input.GetAxis("Horizontal");
            if (playerData.moveX != 0)
            {
                playerData.rb2D.velocity = new Vector2(playerData.moveX * playerData.currentSpeed, playerData.rb2D.velocity.y);
                playerData.animator.SetBool("isWalking", true);
            }
            else
            {
                playerData.animator.SetBool("isWalking", false);
                stateManager.SwitchState(stateManager.IdleState);
            }

            // Jump
            if (playerData.isGrounded && Input.GetButtonDown("Jump"))
            {
                playerData.animator.SetBool("isWalking", false);
                stateManager.SwitchState(stateManager.JumpingState);
            }

            // Attack
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
                playerData.animator.SetBool("isWalking", false);
                stateManager.SwitchState(stateManager.DashingState);
            }

            // Fall
            if (!playerData.isGrounded)
            {
                playerData.animator.SetBool("isWalking", false);
                stateManager.SwitchState(stateManager.FallingState);
            }
        }
    }
}
