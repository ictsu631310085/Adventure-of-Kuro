using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private bool _startJump;
    private float _prepareJumpTimerValue = 0.25f;
    private float _prepareJumpTimer;
    private float _jumpTimer;

    // Start
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.Jumping;

        _startJump = false;
        _prepareJumpTimer = _prepareJumpTimerValue;
        _jumpTimer = playerData.jumpTime;

        playerData.animator.SetTrigger("Jump");
    }

    // Update
    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        // Prepare to Jump
        if (_prepareJumpTimer > 0)
        {
            _prepareJumpTimer -= Time.deltaTime;
        }
        // Start Jumping
        else if (!_startJump)
        {
            playerData.rb2D.velocity = Vector2.up * playerData.jumpForce;
            playerData.jumpRemain--;
            playerData.isJumping = true;
            playerData.jumpAttackEnabled = true;

            _startJump = true;
        }

        if (_startJump)
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

            // Double Jump
            if (Time.timeScale == 1 && playerData.currentElement == EnumHolder.Element.Ventus && playerData.jumpRemain > 0 && _startJump && Input.GetButtonDown("Jump"))
            {
                stateManager.SwitchState(stateManager.DoubleJumpingState);
            }

            // Attack
            if (Time.timeScale == 1 && playerData.jumpAttackEnabled && Input.GetButtonDown("Attack"))
            {
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

            // Flip Sprite When Turn
            playerData.TurnCharacter();

            // Ventus Air Dash
            if (Time.timeScale == 1 && playerData.currentElement == EnumHolder.Element.Ventus && playerData.currentMP > 0 && Input.GetButtonDown("Ability1"))
            {
                playerData.currentMP--;
                playerData.isJumping = false;
                stateManager.SwitchState(stateManager.DashingState);
            }
        }

        // Move
        playerData.moveX = Input.GetAxis("Horizontal");
        playerData.rb2D.velocity = new Vector2(playerData.moveX * playerData.currentSpeed, playerData.rb2D.velocity.y);

        // Fall
        if (playerData.rb2D.velocity.y < -0.5)
        {
            stateManager.SwitchState(stateManager.FallingState);
        }
    }
}
