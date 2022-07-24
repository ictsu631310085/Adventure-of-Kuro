using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager stateManager, PlayerData playerData)
    {
        stateManager.currentState = PlayerStateManager.State.Dashing;

        playerData.animator.SetBool("isDashing", true);
        playerData.InstDashFX();
    }

    public override void UpdateState(PlayerStateManager stateManager, PlayerData playerData)
    {
        // Get Stunned
        if (playerData.stunTime > 0)
        {
            playerData.rb2D.velocity = Vector2.zero;
            playerData.dashTimer = playerData.dashTimeValue;
            playerData.animator.SetBool("isDashing", false);
            stateManager.SwitchState(stateManager.IdleState);
        }
        // Not Get Stunned
        else
        {
            if (playerData.dashTimer > 0)
            {
                playerData.dashTimer -= Time.deltaTime;
                switch (playerData.currentElement)
                {
                    case EnumHolder.Element.Ignis:
                        switch (playerData.facing)
                        {
                            case EnumHolder.Facing.Right:
                                playerData.rb2D.velocity = Vector2.left * playerData.dashSpeed;
                                break;
                            case EnumHolder.Facing.Left:
                                playerData.rb2D.velocity = Vector2.right * playerData.dashSpeed;
                                break;
                        }
                        break;
                    case EnumHolder.Element.Ventus:
                        switch (playerData.facing)
                        {
                            case EnumHolder.Facing.Right:
                                playerData.rb2D.velocity = Vector2.right * playerData.dashSpeed;
                                break;
                            case EnumHolder.Facing.Left:
                                playerData.rb2D.velocity = Vector2.left * playerData.dashSpeed;
                                break;
                        }
                        break;
                }
            }
            else
            {
                playerData.rb2D.velocity = Vector2.zero;
                playerData.dashTimer = playerData.dashTimeValue;
                playerData.animator.SetBool("isDashing", false);
                stateManager.SwitchState(stateManager.IdleState);
            }
        }
    }
}
