using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpingState : PlayerBaseState
{
    private float _jumpTimer;

    // Start
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.DoubleJumping;

        playerData.rb2D.velocity = Vector2.up * playerData.jumpForce;
        _jumpTimer = playerData.jumpTime;

        playerData.jumpRemain--;
        playerData.isJumping = true;
        playerData.jumpAttackEnabled = true;

        playerData.animator.SetTrigger("Jump");
        playerData.InstDoubleJumpFX();
    }

    // Update
    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        // Jump Higher
        if (playerData.isJumping && Input.GetButton("Jump"))
        {
            if (_jumpTimer > 0)
            {
                playerData.rb2D.velocity = Vector2.up * playerData.jumpForce;
                _jumpTimer -= Time.deltaTime;
            }
            else
            {
                playerData.isJumping = false;
            }
        }
        // Stop Jump Higher
        else if (Input.GetButtonUp("Jump"))
        {
            playerData.isJumping = false;
        }

        // Attack
        if (playerData.jumpAttackEnabled && Input.GetButtonDown("Attack"))
        {
            stateManager.SwitchState(stateManager.JumpAttackingState);
        }

        // Shift Element
        // Right
        if (Input.GetButtonDown("Shift Element Right"))
        {
            playerData.ShiftElementRight();
        }
        // Left
        else if (Input.GetButtonDown("Shift Element Left"))
        {
            playerData.ShiftElementLeft();
        }

        // Turning
        playerData.TurnCharacter();

        // Ventus Air Dash
        if (playerData.currentElement == EnumHolder.Element.Ventus && playerData.currentMP > 0 && Input.GetButtonDown("Ability1"))
        {
            playerData.currentMP--;
            playerData.isJumping = false;
            stateManager.SwitchState(stateManager.DashingState);
        }

        // Move
        playerData.moveX = Input.GetAxis("Horizontal");
        playerData.rb2D.velocity = new Vector2(playerData.moveX * playerData.currentSpeed, playerData.rb2D.velocity.y);

        // Flip Sprite When Turn
        playerData.TurnCharacter();

        // Fall
        if (playerData.rb2D.velocity.y < -0.5)
        {
            stateManager.SwitchState(stateManager.FallingState);
        }
    }
}
