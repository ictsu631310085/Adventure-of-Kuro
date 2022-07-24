using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttackingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.JumpAttacking;

        playerData.jumpAttackEnabled = false;
        playerData.isJumpAttack = true;
        playerData.MeleeHitCheck();
        playerData.animator.SetTrigger("JumpAttack");
    }

    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        // Move
        playerData.moveX = Input.GetAxis("Horizontal");
        playerData.rb2D.velocity = new Vector2(playerData.moveX * playerData.currentSpeed, playerData.rb2D.velocity.y);

        if (!playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ignis Jump Attack") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Terra Jump Attack") &&
            !playerData.animator.GetCurrentAnimatorStateInfo(0).IsName("Ventus Jump Attack"))
        {
            playerData.isJumpAttack = false;

            if (playerData.isGrounded)
            {
                playerData.jumpRemain = playerData.jumpRemainValue;
                playerData.animator.SetBool("isFalling", false);
                stateManager.SwitchState(stateManager.IdleState);
            }
            else
            {
                stateManager.SwitchState(stateManager.FallingState);
            }
        }
    }
}
