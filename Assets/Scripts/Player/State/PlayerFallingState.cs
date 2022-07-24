using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.Falling;

        playerData.isJumping = false;
        playerData.animator.SetBool("isFalling", true);
    }

    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        // Get Stunned
        if (playerData.stunTime > 0)
        {
            playerData.jumpRemain = playerData.jumpRemainValue;
            playerData.jumpAttackEnabled = false;

            playerData.animator.SetBool("isFalling", false);

            stateManager.SwitchState(stateManager.IdleState);
        }
        // Not Get Stunned
        else
        {
            // Move
            playerData.moveX = Input.GetAxis("Horizontal");
            playerData.rb2D.velocity = new Vector2(playerData.moveX * playerData.currentSpeed, playerData.rb2D.velocity.y);

            // Flip Sprite When Turn
            playerData.TurnCharacter();

            // Jump
            if (Time.timeScale == 1 && playerData.jumpRemain > 0 && Input.GetButtonDown("Jump"))
            {
                playerData.animator.SetBool("isFalling", false);

                if (playerData.currentElement == EnumHolder.Element.Ventus)
                {
                    stateManager.SwitchState(stateManager.DoubleJumpingState);
                }
                else
                {
                    stateManager.SwitchState(stateManager.JumpingState);
                }
            }

            // Attack
            if (Time.timeScale == 1 && playerData.jumpAttackEnabled && Input.GetButtonDown("Attack"))
            {
                playerData.animator.SetBool("isFalling", false);

                stateManager.SwitchState(stateManager.JumpAttackingState);
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

            // Ventus Air Dash
            if (Time.timeScale == 1 && playerData.currentElement == EnumHolder.Element.Ventus && playerData.currentMP > 0 && Input.GetButtonDown("Ability1"))
            {
                playerData.currentMP--;
                stateManager.SwitchState(stateManager.DashingState);
            }
        }

        // Land
        if (playerData.isGrounded)
        {
            playerData.jumpRemain = playerData.jumpRemainValue;
            playerData.jumpAttackEnabled = false;

            playerData.animator.SetBool("isFalling", false);

            stateManager.SwitchState(stateManager.IdleState);
        }
    }
}
